using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class StarAdventure : Solution<string[], int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new[]
{"01",
 "11"}
, Output = 3 },
                new TestInput { Input = new[]
{"0999999999"
,"9999999999"
,"9999999999"
,"9999999999"
,"9999999999"
,"9999999999"
,"9999999999"
,"9999999999"
,"9999999999"
,"9999999999"}
, Output = 450 },
                new TestInput { Input = new[]
{"012"
,"012"
,"012"
,"012"
,"012"
,"012"
,"012"}
, Output = 21 },
                new TestInput { Input = new[]
{"0123456789",
 "1123456789",
 "2223456789",
 "3333456789",
 "4444456789",
 "5555556789",
 "6666666789",
 "7777777789",
 "8888888889",
 "9999999999"}
, Output = 335 },
            };
        }

        public override int Do(string[] input)
        {
            int m = input.Length;
            int n = input[0].Length;

            var totalSum = 0;

            var grid = new int[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    grid[i, j] = int.Parse(input[i][j] + "");
                    totalSum += grid[i, j];
                }

            if (m <= 3 || n <= 3) return totalSum;

            var prev = new int[n, n, n];

            for (int row = 0; row < m; row++)
            {
                var cur = new int[n, n, n];

                for (int k = 0; k < n; k++)
                    for (int j = 0; j < k; j++)
                        for (int i = 0; i < j; i++)
                            cur[i, j, k] = Math.Max(cur[i, j, k], prev[i, j, k] + grid[row, i] + grid[row, j] + grid[row, k]);

                for (int k = 3; k < n; k++)
                    for (int j = 2; j < k; j++)
                        for (int i = 1; i < j; i++)
                            cur[i, j, k] = Math.Max(cur[i, j, k], cur[i - 1, j, k] + grid[row, i]);

                for (int k = 3; k < n; k++)
                    for (int i = 0; i < k - 2; i++)
                        for (int j = i + 2; j < k; j++)
                            cur[i, j, k] = Math.Max(cur[i, j, k], cur[i, j - 1, k] + grid[row, j]);

                for (int i = 0; i < n - 3; i++)
                    for (int j = i + 1; j < n - 2; j++)
                        for (int k = j + 2; k < n; k++)
                            cur[i, j, k] = Math.Max(cur[i, j, k], cur[i, j, k - 1] + grid[row, k]);

                prev = cur;
            }

            var max = 0;
            for (int k = 0; k < n; k++)
                for (int j = 0; j < k; j++)
                    for (int i = 0; i < j; i++)
                        if (prev[i, j, k] > max) max = prev[i, j, k];

            return max;
        }


    }
}
