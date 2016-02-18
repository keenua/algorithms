using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class Jewelry : Solution<int[], long>
    {
        const int MaxValue = 1000;

        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new[] {1,2,5,3,4,5}, Output = 9 },
                new TestInput { Input = new [] {1000,1000,1000,1000,1000,
 1000,1000,1000,1000,1000,
 1000,1000,1000,1000,1000,
 1000,1000,1000,1000,1000,
 1000,1000,1000,1000,1000,
 1000,1000,1000,1000,1000}, Output = 18252025766940 },
                new TestInput { Input = new[]{1,2,3,4,5}, Output = 4},
                new TestInput { Input = new[]{7,7,8,9,10,11,1,2,2,3,4,5,6}, Output = 607},
                new TestInput { Input = new[]{123,217,661,678,796,964,54,111,417,526,917,923}, Output = 0}
            };
        }

        public override long Do(int[] input)
        {
            var list = input.ToList();
            list.Sort();

            var n = list.Count;
            int max = list.Sum();

            long[,] bin = new long[n + 1, n + 1];
            for (int i = 0; i <= n; i++)
            {
                bin[i, 0] = 1;

                for (int j = 1; j <= i; j++)
                    bin[i, j] = bin[i - 1, j] + bin[i - 1, j - 1];
            }

            var waysBelow = new long[max + 1, n + 1];
            var waysAbove = new long[max + 1, n + 1];

            waysBelow[0, 0] = 1;
            waysAbove[0, 0] = 1;

            for (int s = 0; s <= max; s++)
            {
                for (int i = 1; i <= n; i++)
                {
                    waysBelow[s, i] = waysBelow[s, i - 1];
                    if (s >= list[i - 1])
                        waysBelow[s, i] += waysBelow[s - list[i - 1], i - 1];

                    waysAbove[s, i] = waysAbove[s, i - 1];
                    if (s >= list[n - i])
                        waysAbove[s, i] += waysAbove[s - list[n - i], i - 1];
                }
            }

            long result = 0;

            for (int i = 0; i < n; i++)
            {
                int len = 1;
                while (i + len < n && list[i + len] == list[i])
                    len++;

                for (int j = i; j <= i + len - 1; j++)
                    for (int s = (j - i + 1) * list[i]; s <= max; s++)
                        result += bin[len, j - i + 1] * waysBelow[s - (j - i + 1) * list[i], i] * waysAbove[s, n - 1 - j];

                i += len - 1;
            }

            return result;
        }
    }
}
