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
