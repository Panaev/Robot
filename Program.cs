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
            var graph = Init.GraphInit();

			PositionSensorsData sensorsData = null;

            sensorsData = server.SendCommand(new Command { LinearVelocity = 10, Time = 1 });
            sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromGrad(-90), Time = 1 });
            sensorsData = server.SendCommand(new Command { LinearVelocity = 10, Time = 1 });
            bool mainloop = true;
            while (mainloop)
            {
                // граф должен инициализироваться каждый раз, т.к. карта всё время разная!
                // инициализируем граф

                double minDist = 361;
                double robotX = sensorsData.Position.PositionsData.X; // как это пишется?
                double robotY = sensorsData.Position.PositionsData.Y; // и это
                double k = sensorsData.Position.PositionsData.Angle; // и это
                double objX, objY = 0;
                var compliance = new Dictionary<string, string>();
                compliance.Add("RedDetail", "HorizontalRedSocket");
                compliance.Add("RedDetail", "VerticalRedSocket");
                compliance.Add("BlueDetail", "HorizontalBlueSocket");
                compliance.Add("BlueDetail", "VerticalBlueSocket");
                compliance.Add("GreenDetail", "HorizontalGreenSocket");
                compliance.Add("GreenDetail", "VerticalGreenSocket");
                foreach (var e in sensorsData.MapSensor.MapItems)
                {
                    var dist = Math.Sqrt((robotX - e.X) * (robotX - e.X) + (robotY - e.Y) * (robotY - e.Y));
                    if (sensorsData.DetailsInfo.HasGrippedDetail)// если захвачена деталь, и e соответствует ей, или не захвачена, и e - деталь, и при этом dist < minDist
                    {
                        minDist = dist;
                        objX = e.X;
                        objY = e.Y;
                    }
                }

                // определить, какой из вершин принадлежит объект
                // реализовать поиск в ширину кратчайшего пути до объекта с помощью свежепостроенного графа
                // если расстояние до детали больше 50, то найти первый по пути квадрат, присвоить targetX и targetY координаты его центра
                // иначе targetX = objX и targetY = objY

                double deltaX = Math.Abs(robotX - targetX);
                double deltaY = Math.Abs(robotY - targetY);
                double m = Math.Tan(deltaY / deltaX) * 180 / Math.PI;
                double alpha = Math.PI - (k + m);
                if (alpha > Math.PI) alpha = Math.PI - alpha;
                sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromGrad(alpha), Time = 1 });
                sensorsData = server.SendCommand(new Command { LinearVelocity = 50, Time = 1 });

                // если деталь не захвачена, и координаты робота отличаются от координат детали на ~5, то Grip()
                //sensorsData = server.SendCommand(new Command { Action = CommandAction.Grip, Time = 1 });
                // и grippedDetail = эту деталь
                // если деталь захвачена, и координаты соответствующей трубы отличаются от координат робота на ~5, то Release()
                //sensorsData = server.SendCommand(new Command { Action = CommandAction.Release, Time = 1 });

                // если нет доступных объектов, то mainloop = False
            }
			server.Exit();
		}
    }
}
