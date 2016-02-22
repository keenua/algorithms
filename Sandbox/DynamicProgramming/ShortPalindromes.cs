using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class ShortPalindromes : Solution<string, string>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = "RACE", Output = "ECARACE" },
                new TestInput { Input = "TOPCODER", Output = "REDTOCPCOTDER" },
                new TestInput { Input = "Q", Output = "Q" },
                new TestInput { Input = "MADAMIMADAM", Output = "MADAMIMADAM" },
                new TestInput { Input = "ALRCAGOEUAOEURGCOEUOOIGFA", Output = "AFLRCAGIOEOUAEOCEGRURGECOEAUOEOIGACRLFA" }
            };
        }

        private string Min(string a, string b)
        {
            if (a.Length < b.Length) return a;
            else if (a.Length > b.Length) return b;
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] < b[i]) return a;
                    else if (a[i] > b[i]) return b;
                }

                return a;
            }
        }

        private bool IsPalindrome(string str)
        {
            for (int i = 0; i < str.Length / 2; i++)
                if (str[i] != str[str.Length - i - 1]) return false;

            return true;
        }

        public override string Do(string input)
        {
            if (IsPalindrome(input)) return input;

            var n = input.Length;

            var table = new Dictionary<string, string>();

            for (int size = 0; size <= n; size++)
                for (int index = 0; index <= n - size; index++)
                {
                    var str = input.Substring(index, size);

                    if (table.ContainsKey(str)) continue;

                    if (IsPalindrome(str)) table[str] = str;
                    else
                    {
                        if (str[0] == str.Last()) table[str] = str[0] + table[str.Substring(1, str.Length - 2)] + str[0];
                        else
                        {
                            table[str] = Min(str.Last() + table[str.Substring(0, str.Length - 1)] + str.Last(), str[0] + table[str.Substring(1)] + str[0]);
                        }
                    }
                }

            return table[input];
        }
    }
}
