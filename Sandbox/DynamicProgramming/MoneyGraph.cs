using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class MoneyGraphInput
    {
        public string Graph { get; set; }
        public int M { get; set; }
        public int[] S { get; set; }
    }

    public class MoneyGraph : Solution<MoneyGraphInput, List<int>>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new MoneyGraphInput { Graph = "0 2 3 0\r\n2 0 2 2\r\n3 2 0 2\r\n0 2 2 0", M = 100, S = new[] { 5, 5, 5, 5 } }, Output = new List<int> { 0, 1, 3 } },
                new TestInput { Input = new MoneyGraphInput { Graph = "0 2 3 0\r\n2 0 2 2\r\n3 2 0 2\r\n0 2 2 0", M = 10, S = new[] { 1, 100, 1, 1 } }, Output = new List<int> { 0, 2, 3 } },
                new TestInput { Input = new MoneyGraphInput { Graph = "0 2 3 0\r\n2 0 2 2\r\n3 2 0 2\r\n0 2 2 0", M = 10, S = new[] { 100, 100, 100, 100 } }, Output = new List<int> { } }
            };
        }

        public override List<int> Do(MoneyGraphInput input)
        {
            var graph = input.Graph.ToGraph();

            var n = graph.GetLength(0);

            var visited = new bool[n, input.M + 1];
            var min = new int[n, input.M + 1];
            var prev = new int[n, input.M + 1];

            for (int i = 0; i < n; i++)
                for (int j = 0; j <= input.M; j++)
                {
                    min[i, j] = int.MaxValue;
                    prev[i, j] = -1;
                }

            min[0, input.M] = 0;

            while (true)
            {
                int k = 0, l = 0;
                var currentMin = int.MaxValue;

                for (int i = 0; i < n; i++)
                    for (int j = 0; j <= input.M; j++)
                    {
                        if (visited[i, j]) continue;

                        if (min[i,j] != int.MaxValue && min[i,j] < currentMin)
                        {
                            k = i;
                            l = j;
                            currentMin = min[i, j];
                        }
                    }

                if (currentMin == int.MaxValue) break;

                visited[k, l] = true;

                for (int i = 0; i < n; i++)
                    if (graph[k, i] > 0)
                    {
                        var moneyLeft = l - input.S[i];

                        if (moneyLeft >= 0 && min[i, moneyLeft] > min[k, l] + graph[k, i])
                        {
                            min[i, moneyLeft] = min[k, l] + graph[k, i];
                            prev[i, moneyLeft] = k;
                        }
                    }
            }

            int minPath = int.MaxValue;
            int maxMoney = int.MaxValue;

            for (int j = input.M; j >= 0; j--)
            {
                if (min[n - 1, j] != int.MaxValue)
                {
                    if (minPath > min[n - 1, j])
                    {
                        minPath = min[n - 1, j];
                        maxMoney = j;
                    }
                }
            }

            if (maxMoney != int.MaxValue)
            {
                var previous = new int[n];

                int current = n - 1;
                var money = maxMoney;

                var result = new List<int>();

                result.Add(current);

                while (prev[current, money] != -1)
                {
                    var newMoney = money + input.S[current];

                    current = prev[current, money];
                    result.Add(current);

                    money = newMoney;
                }

                result.Reverse();
                return result;
            }

            return new List<int>();
        }
    }
}
