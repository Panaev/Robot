using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
//пока обычный проход по графу, ничего не учитывающий (копипаст с лекций и пара исправлений).
//нам наверно понадобится поиск в ширину, так что первый метод не нужен, он работает, вершины выводятся в правильном порядке
    public class Search 
    {
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
        public static IEnumerable<Node> DepthSearch(Node startNode)
        {
            var visited = new HashSet<Node>();
            var stack = new Stack<Node>();
            stack.Push(startNode);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                if (visited.Contains(node)) continue;
                visited.Add(node);
                yield return node;
                foreach (var incidentNode in node.incidentNodes)
                    stack.Push(incidentNode);
            }
        }

        public static IEnumerable<Node> BreadthSearch(Node startNode)
        {
            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (visited.Contains(node)) continue;
                visited.Add(node);
                yield return node;
                foreach (var incidentNode in node.incidentNodes)
                    queue.Enqueue(incidentNode);
            }
        }
    }
}
