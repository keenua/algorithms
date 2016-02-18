using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class ZigZag : Solution<int[], int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new int[] { 1, 7, 4, 9, 2, 5 }, Output = 6 },
                new TestInput { Input = new int[] { 1, 17, 5, 10, 13, 15, 10, 5, 16, 8 }, Output = 7 },
                new TestInput { Input = new int[] { 44 }, Output = 1 },
                new TestInput { Input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, Output = 2 },
                new TestInput { Input = new int[] { 70, 55, 13, 2, 99, 2, 80, 80, 80, 80, 100, 19, 7, 5, 5, 5, 1000, 32, 32 }, Output = 8 },
                new TestInput { Input = new int[] { 374, 40, 854, 203, 203, 156, 362, 279, 812, 955,
600, 947, 978, 46, 100, 953, 670, 862, 568, 188,
67, 669, 810, 704, 52, 861, 49, 640, 370, 908,
477, 245, 413, 109, 659, 401, 483, 308, 609, 120,
249, 22, 176, 279, 23, 22, 617, 462, 459, 244 }, Output = 36 },
            };
        }

        public override int Do(int[] input)
        {
            if (input.Length < 3) return input.Length;

            var matrix = new int[input.Length, 2];

            matrix[0, 0] = 1;
            matrix[0, 1] = 1;

            for (int i = 1; i < input.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (input[i] > input[j])
                        matrix[i, 1] = Math.Max(matrix[j, 0] + 1, matrix[i, 1]);
                    else if (input[i] < input[j])
                        matrix[i, 0] = Math.Max(matrix[j, 1] + 1, matrix[i, 0]);
                }
            }

            int max = int.MinValue;

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < 2; j++)
                    max = Math.Max(matrix[i, j], max);

            return max;
        }
    }
}
