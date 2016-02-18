using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class AvoidRoadsInput
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string[] Bad { get; set; }
    }

    public class AvoidRoads : Solution<AvoidRoadsInput, long>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new AvoidRoadsInput { Width = 6, Height = 6, Bad = new[] {"0 0 0 1","6 6 5 6"} }, Output = 252 },
                new TestInput { Input = new AvoidRoadsInput { Width = 1, Height = 1, Bad = new string[0] }, Output = 2 },
                new TestInput { Input = new AvoidRoadsInput { Width = 35, Height = 31, Bad = new string[0] }, Output = 6406484391866534976 },
                new TestInput { Input = new AvoidRoadsInput { Width = 2, Height = 2, Bad = new string[] {"0 0 1 0", "1 2 2 2", "1 1 2 1"}}, Output = 0 }
            };
        }

        private bool IsBad(AvoidRoadsInput input, int x1, int y1, int x2, int y2)
        {
            return input.Bad.Contains(string.Join(" ", x1, y1, x2, y2)) || input.Bad.Contains(string.Join(" ", x2, y2, x1, y1));
        }

        public override long Do(AvoidRoadsInput input)
        {
            long[,] dp = new long[input.Width + 1, input.Height + 1];

            dp[0, 0] = 1;

            for (int i = 0; i < input.Width + 1; i++)
                for (int j = 0; j < input.Height + 1; j++)
                {
                    if (i < input.Width && !IsBad(input, i, j, i + 1, j))
                        dp[i + 1, j] += dp[i, j];

                    if (j < input.Height && !IsBad(input, i, j, i, j + 1))
                        dp[i, j + 1] += dp[i, j];
                }

            return dp[input.Width, input.Height];
        }
    }
}
