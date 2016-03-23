using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Strings
{
    public class MorrisPrattAlgorithm : SubstringSearchSolution
    {
        private int[] BuildFailureFunction(string pattern)
        {
            var f = new int[pattern.Length + 1];

            f[0] = 0;
            if (f.Length > 1) f[1] = 0;

            for (int i = 2; i < f.Length; i++)
            {
                var j = f[i - 1];

                while (true)
                {
                    if (pattern[j] == pattern[i - 1])
                    {
                        f[i] = j + 1;
                        break;
                    }

                    if (j == 0)
                    {
                        f[i] = 0;
                        break;
                    }

                    j = f[j];
                }
            }

            return f;
        }

        public override List<int> Do(SubstringSearchSolutionInput input)
        {
            var result = new List<int>();

            var f = BuildFailureFunction(input.Pattern);

            var i = 0;
            var j = 0;

            while (true)
            {
                if (j == input.Str.Length) break;

                if (input.Str[j] == input.Pattern[i])
                {
                    i++;
                    j++;
                    if (i == input.Pattern.Length)
                    {
                        result.Add(j - input.Pattern.Length);
                        i = 0;
                        j -= input.Pattern.Length - 1;
                    }
                }
                else if (i > 0)
                {
                    i = f[i];
                }
                else j++;
            }

            return result;
        }
    }
}
