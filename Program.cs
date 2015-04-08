using System;
using System.Collections.Generic;
using AIRLab.Mathematics;
using ClientBase;
using CommonTypes;
using CVARC.Basic.Controllers;
using CVARC.Network;
using RepairTheStarship.Sensors;
using System.Linq;

namespace Robot
{
    internal class Program
    {


        private static readonly ClientSettings Settings = new ClientSettings
        {
            Side = Side.Left, //Переключив это поле, можно отладить алгоритм для левой или правой стороны, а также для произвольной стороны, назначенной сервером
            LevelName = LevelName.Level1, //Задается уровень, в котором вы хотите принять участие
            MapNumber = -1 //Задавая различные значения этого поля, вы можете сгенерировать различные случайные карты
        };

        private static void Main(string[] args)
		{
			var server = new CvarcClient(args, Settings).GetServer<PositionSensorsData>();
			var helloPackageAns = server.Run();
			PositionSensorsData sensorsData = null;

            sensorsData = server.SendCommand(new Command { LinearVelocity = 10, Time = 1 });
            sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromGrad(-90), Time = 1 });
            sensorsData = server.SendCommand(new Command { LinearVelocity = 10, Time = 1 });
            bool mainloop = true;
            
            var graph = new Graph(24);
            int t = 0;
            for (int i = 75; i >= -75; i -= 50)
                for (int j = -125; j <= 125; j += 50)
                {
                    graph[t].X = i;
                    graph[t].Y = j;
                    t++;
                }
            for (int i = 0; i < graph.Length; i++)
            {
                var plusX = i + 1;
                var plusY = i + 6;
                var minusX = i - 1;
                var minusY = i - 6;
                bool addPlusX = true;
                bool addMinusX = true;
                bool addPlusY = true;
                bool addMinusY = true;
                foreach (var e in sensorsData.MapSensor.MapItems)
                {
                    if (plusX >= 0 && plusX < 24 && e.X == graph[plusX].X)
                        addPlusX = false;
                    if (plusY >= 0 && plusY < 24 && e.Y == graph[plusY].Y)
                        addPlusY = false;
                    if (minusX >= 0 && minusX < 24 && e.X == graph[minusX].X)
                        addMinusX = false;
                    if (minusY >= 0 && minusY < 24 && e.Y == graph[minusX].Y)
                        addMinusY = false;
                }
                if (addPlusX && plusX >= 0 && plusX < 24) graph.Connect(i, plusX);
                if (addMinusX && minusX >= 0 && minusX < 24) graph.Connect(i, minusX);
                if (addPlusY && plusY >= 0 && plusY < 24) graph.Connect(i, plusY);
                if (addMinusY && minusY >= 0 && minusY < 24) graph.Connect(i, minusY);
            }
            int robotIndex = helloPackageAns.RealSide == Side.Left ? 1 : 0;

            while (mainloop)
            {
                double minDist = 361;
                double robotX = sensorsData.Position.PositionsData[robotIndex].X;
                double robotY = sensorsData.Position.PositionsData[robotIndex].Y;
                double k = sensorsData.Position.PositionsData[robotIndex].Angle;
                if (k >= Math.PI) k = Math.PI - k;
                double objX = 0;
                double objY = 0;
                foreach (var e in sensorsData.MapSensor.MapItems)
                {
                    var dist = Math.Sqrt((robotX - e.X) * (robotX - e.X) + (robotY - e.Y) * (robotY - e.Y));
                    if (dist < minDist &&
                        (!sensorsData.DetailsInfo.HasGrippedDetail && (e.Tag == "RedDetail" || e.Tag == "GreenDetail" || e.Tag == "BlueDetail") ||
                        sensorsData.DetailsInfo.HasGrippedDetail && (e.Tag == "HorizontalRedSocket" ||
                        e.Tag == "VerticalRedSocket" || e.Tag == "HorizontalGreenSocket" ||
                        e.Tag == "VerticalGreenSocket" || e.Tag == "HorizontalBlueSocket" || e.Tag == "VerticalBlueSocket"
                        || e.Tag == "HorizontalWall" || e.Tag == "VerticalWall")))
                    {
                        minDist = dist;
                        objX = e.X;
                        objY = e.Y;
                    }
                }

                double minDistTarget = 0;
                int targetNodeIndex = 0;
                for (int i = 0; i < graph.Length; i++)
                {
                    var dist = Math.Sqrt((graph[i].X - objX) * (graph[i].X - objX) + (graph[i].Y - objY) * (graph[i].Y - objY));
                    if (dist < minDistTarget)
                    {
                        dist = minDistTarget;
                        targetNodeIndex = i;
                    }
                }
                double minDistRobot = 0;
                int robotNodeIndex = 0;
                for (int i = 0; i < graph.Length; i++)
                {
                    var dist = Math.Sqrt((graph[i].X - objX) * (graph[i].X - objX) + (graph[i].Y - objY) * (graph[i].Y - objY));
                    if (dist < minDistRobot)
                    {
                        dist = minDistRobot;
                        robotNodeIndex = i;
                    }
                }
                List<Node> path = FindPath(graph[robotNodeIndex], graph[targetNodeIndex]);

                double targetX = graph[targetNodeIndex].X;
                double targetY = graph[targetNodeIndex].Y;
                double robotObjectDist = Math.Sqrt((robotX - objX) * (robotX - objX) + (robotY - objY) * (robotY - objY));
                if (robotObjectDist <= 25)
                {
                    targetX = objX;
                    targetY = objY;
                }

                if (Math.Sqrt((robotX - targetX) * (robotX - targetX) + (robotY - targetY) * (robotY - targetY)) <= 25)
                {
                    targetX = path[1].X;
                    targetY = path[1].Y;
                }

                double deltaX = Math.Abs(robotX - targetX);
                double deltaY = Math.Abs(robotY - targetY);
                double m = Math.Atan(deltaY / deltaX);
                double alpha = Math.PI - (k + m);
                if (alpha > Math.PI) alpha = -(Math.PI - alpha);
                Console.WriteLine(robotObjectDist);
                if (!sensorsData.DetailsInfo.HasGrippedDetail && robotObjectDist < 15)
                    sensorsData = server.SendCommand(new Command { Action = CommandAction.Grip, Time = 1 });
                if (alpha != 0) sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromRad(alpha), Time = 1 });
                sensorsData = server.SendCommand(new Command { LinearVelocity = 50, Time = 1 });
                // и grippedDetail = эту деталь
                // если деталь захвачена, и координаты соответствующей трубы отличаются от координат робота на ~5, то Release()
                //sensorsData = server.SendCommand(new Command { Action = CommandAction.Release, Time = 1 });

                // если нет доступных объектов, то mainloop = False
            }
			server.Exit();
		}

        public static List<Node> FindPath(Node start, Node end)
        {
            var track = new Dictionary<Node, Node>();
            track[start] = null;
            var queue = new Queue<Node>();
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                foreach (var nextNode in node.incidentNodes)
                {
                    if (track.ContainsKey(nextNode)) continue;
                    track[nextNode] = node;
                    queue.Enqueue(nextNode);
                }
                if (track.ContainsKey(end)) break;
            }
            var pathItem = end;
            var result = new List<Node>();
            while (pathItem != null)
            {
                result.Add(pathItem);
                pathItem = track[pathItem];
            }
            result.Reverse();
            return result;
        }
    }
}
