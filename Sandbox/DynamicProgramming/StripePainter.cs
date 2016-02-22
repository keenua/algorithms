using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class StripePainter : Solution<string, int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = "RGBGR", Output = 3 },
                new TestInput { Input = "RGRG", Output = 3 },
                new TestInput { Input = "ABACADA", Output = 4 },
                new TestInput { Input = "AABBCCDDCCBBAABBCCDD", Output = 7 },
                new TestInput { Input = "BECBBDDEEBABDCADEAAEABCACBDBEECDEDEACACCBEDABEDADD", Output = 26 }
            };
        }

        public override int Do(string input)
        {
            var n = input.Length;

            var min = new int[n + 1, n + 1, 128];

            for (int size = 0; size <= n; size++)
                for (int left = 0; left <= n - size; left++)
                    for (var color = '@'; color <= 'Z'; color++)
                    {
                        if (size == 0) min[left, size, color] = 0;
                        else
                        {
                            if (input[left] == color)
                                min[left, size, color] = min[left + 1, size - 1, color];
                            else
                            {
                                var minSum = int.MaxValue;

                                for (int i = 1; i <= size; i++)
                                {
                                    var sum = 1 + min[left + 1, i - 1, input[left]] + min[left + i, size - i, color];

                                    if (sum < minSum) minSum = sum;
                                }

                                min[left, size, color] = minSum;
                            } 
                        }
                    }

            return min[0, input.Length, '@'];
        }
    }
}
