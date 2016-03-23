using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class ShortestPath : Solution<string, List<int>>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = "0 5\r\n-5 0", Output = new List<int> { 0, 1 } },
                new TestInput { Input = "0 -1 2 2 -1 -1\r\n-1 0 4 -1 1 -1\r\n2 4 0 2 -1 -1\r\n2 -1 2 0 1 -1\r\n-1 1 -1 1 0 8\r\n-1 -1 -1 -1 8 0", Output = new List<int> { 0, 3, 4, 5 } },
                new TestInput { Input = "0 -1\r\n-1 0", Output = new List<int> { } }
            };
        }

        public override List<int> Do(string g)
        {
            var graph = g.ToGraph();

            var n = graph.GetLength(0);

            var queue = Enumerable.Range(0, n).ToList();
            var distance = new int[n];
            var previous = new int[n];

            for (int i = 0; i < n; i++)
            {
                distance[i] = int.MaxValue;
                previous[i] = -1;
            }

            distance[0] = 0;

            while (queue.Any())
            {
                int min = -1;

                foreach (var q in queue)
                    if ((min == -1 || distance[q] < distance[min]) &&  distance[q] != -1)
                        min = q;

                if (min == n - 1)
                    return previous.ToPath();

                for (int i = 0; i < n; i++)
                {
                    if (i == min || graph[i, min] == -1) continue;

                    var dist = graph[i, min] + distance[min];

                    if (dist < distance[i])
                    {
                        distance[i] = dist;
                        previous[i] = min;
                    }
                }

                queue.Remove(min);
            }

            if (distance[n - 1] == -1) return new List<int>();

            return previous.ToPath();
        }
    }
}
