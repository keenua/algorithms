using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class NonDecreasing : Solution<int[], int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new [] { 5, 3, 4, 8, 6, 7 }, Output = 4 },
                new TestInput { Input = new [] { 4 }, Output = 1 },
                new TestInput { Input = new [] { 1, 8, 2, 9 }, Output = 3 },
                new TestInput { Input = new int[0], Output = 0 },
                new TestInput { Input = new [] { 0, 1, 1, 8, 1, 5 }, Output = 5 }
            };
        }

        public override int Do(int[] input)
        {
            if (!input.Any()) return 0;

            var matrix = new int[input.Length];

            matrix[0] = 1;

            for (var i = 1; i < input.Length; i++)
            {
                matrix[i] = 1;

                for (int j = 0; j < i; j++)
                    if (input[j] <= input[i])
                        matrix[i] = Math.Max(matrix[i], matrix[j] + 1);
            }

            return matrix[input.Length - 1];
        }
    }
}
