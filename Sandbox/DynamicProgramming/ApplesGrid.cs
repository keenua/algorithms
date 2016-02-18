using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class ApplesGrid : Solution<string, int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = "1 4\r\n5 2", Output = 8 },
                new TestInput { Input = "1 2 3\r\n4 5 6\r\n7 8 9", Output = 29 }
            };
        }

        public override int Do(string input)
        {
            var grid = input.ToGraph();

            var m = grid.GetLength(0);
            var n = grid.GetLength(1);

            var dp = new int[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    dp[i, j] = grid[i, j] + Math.Max(i > 0 ? dp[i - 1, j] : 0, j > 0 ? dp[i, j - 1] : 0);

            return dp[m - 1, n - 1];
        }
    }
}
