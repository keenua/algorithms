using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class BadNeighbors : Solution<int[], int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new int[] { 10, 3, 2, 5, 7, 8 }, Output = 19 },
                new TestInput { Input = new int[] { 11, 15 }, Output = 15 },
                new TestInput { Input = new int[] { 7, 7, 7, 7, 7, 7, 7 }, Output = 21 },
                new TestInput { Input = new int[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 }, Output = 16 },
                new TestInput { Input = new int[] { 94, 40, 49, 65, 21, 21, 106, 80, 92, 81, 679, 4, 61,
  6, 237, 12, 72, 74, 29, 95, 265, 35, 47, 1, 61, 397,
  52, 72, 37, 51, 1, 81, 45, 435, 7, 36, 57, 86, 81, 72 }, Output = 2926 }
            };
        }

        public override int Do(int[] input)
        {
            int[,] matrix = new int[input.Length, 2];

            for (int i = 0; i < input.Length; i++)
            {
                matrix[i, 0] = input[i];
                matrix[i, 1] = i == 0 ? 0 : input[i];
            }

            for (int i = 2; i < input.Length; i++)
                for (int j = 0; j < i - 1; j++)
                    for (int k = 0; k < 2; k++)
                        matrix[i, k] = Math.Max(matrix[j, k] + input[i], matrix[i, k]);

            int max = 0;

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0 && i == input.Length - 1) continue;

                    max = Math.Max(max, matrix[i, j]);
                }

            return max;
        }
    }
}
