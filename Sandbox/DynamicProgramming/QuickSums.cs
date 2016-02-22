using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class QuickSumsInput
    {
        public int Sum { get; set; }
        public string Numbers { get; set; }
    }

    public class QuickSums : Solution<QuickSumsInput, int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new QuickSumsInput { Numbers = "99999", Sum = 45 }, Output = 4 },
                new TestInput { Input = new QuickSumsInput { Numbers = "1110", Sum = 3 }, Output = 3 },
                new TestInput { Input = new QuickSumsInput { Numbers = "0123456789", Sum = 45 }, Output = 8 },
                new TestInput { Input = new QuickSumsInput { Numbers = "99999", Sum = 100 }, Output = -1 },
                new TestInput { Input = new QuickSumsInput { Numbers = "382834", Sum = 100 }, Output = 2 },
                new TestInput { Input = new QuickSumsInput { Numbers = "9230560001", Sum = 71 }, Output = 4 }
            };
        }

        int[,] CreateMaxArray(int size)
        {
            int[,] result = new int[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] = int.MaxValue;

            return result;
        }

        public override int Do(QuickSumsInput input)
        {
            var max = input.Sum;

            var digits = input.Numbers.Select(x => int.Parse(x + "")).ToArray();

            if (digits.Sum() > max) return -1;

            var prev = CreateMaxArray(max + 1);

            prev[0, digits[0]] = 0;

            for (int d = 1; d < digits.Length; d++)
            {
                var cur = CreateMaxArray(max + 1);

                for (int i = 0; i <= max; i++)
                    for (int j = 0; j <= max; j++)
                        if (prev[i, j] != int.MaxValue)
                        {
                            if (i + j + digits[d] <= max)
                                cur[i + j, digits[d]] = Math.Min(cur[i + j, digits[d]], prev[i, j] + 1);

                            if (i + j * 10 + digits[d] <= max)
                                cur[i, j * 10 + digits[d]] = Math.Min(cur[i, j * 10 + digits[d]], prev[i, j]);
                        }

                prev = cur;
            }

            var min = int.MaxValue;

            for (int i = 0; i <= max; i++)
                min = Math.Min(min, prev[i, max - i]);

            if (min == int.MaxValue) return -1;

            return min;
        }
    }
}
