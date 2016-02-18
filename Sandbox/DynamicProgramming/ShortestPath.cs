using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class ShortestPath : Solution<string, List<int>>
    {
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
