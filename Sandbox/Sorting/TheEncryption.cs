using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class TheEncryption : Solution<string, string>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = "hello", Output = "abccd" },
                new TestInput { Input = "abcd", Output = "abcd" },
                new TestInput { Input = "topcoder", Output = "abcdbefg" },
                new TestInput { Input = "encryption", Output = "abcdefghib" }
            };
        }

        public override string Do(string input)
        {
            var mapping = new char[128];

            var current = 'a';

            var builder = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (mapping[input[i]] == 0)
                    mapping[input[i]] = current++;

                builder.Append(mapping[input[i]]);
            }

            return builder.ToString();
        }
    }
}
