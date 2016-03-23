using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Strings
{

    public class RabinKarpAlgorithm : SubstringSearchSolution
    {
        private const ulong B = 256;
        private const ulong M = 1000004123;

        protected static ulong Mod(ulong a, ulong b)
        {
            return (a % b + b) % b;
        }
        protected static ulong Mod(ulong B, ulong x, ulong M)
        {
            if (x == 0) return 1 % M;
            if (x == 1) return B % M;
            if (x % 2 == 0)
            {
                ulong temp = Mod(B, x / 2, M);
                return (temp * temp) % M;
            }
            else return (Mod(B, x - 1, M) * (B % M)) % M;
        }


        protected static ulong Hash(string text, ulong b, ulong m, int start = 0, int length = -1)
        {
            ulong hash = 0;

            if (length == -1)
                length = text.Length - start;

            for (int i = 0; i < length; i++)
                hash = Mod(hash * b + text[start + i], m);

            return hash;
        }

        private bool EqualsToPattern(string text, int start, string pattern)
        {
            for (int i = 0; i < pattern.Length; i++)
                if (text[i + start] != pattern[i])
                    return false;

            return true;
        }

        protected virtual List<int> Search(SubstringSearchSolutionInput input)
        {
            var result = new List<int>();

            if (input.Pattern.Length > input.Str.Length) return result;

            var patternHash = Hash(input.Pattern, B, M);

            var segmentHash = Hash(input.Str, B, M, 0, input.Pattern.Length);

            if (patternHash == segmentHash && EqualsToPattern(input.Str, 0, input.Pattern))
                result.Add(0);

            var e = Mod(B, (ulong)(input.Pattern.Length - 1), M);

            for (int i = input.Pattern.Length; i < input.Str.Length; i++)
            {
                segmentHash = Mod(segmentHash - Mod(e * input.Str[i - input.Pattern.Length], M), M);
                segmentHash = Mod(segmentHash * B, M);
                segmentHash = Mod(segmentHash + input.Str[i], M);

                if (patternHash == segmentHash && EqualsToPattern(input.Str, i - input.Pattern.Length + 1, input.Pattern))
                    result.Add(i - input.Pattern.Length + 1);
            }

            return result;
        }


        public override List<int> Do(SubstringSearchSolutionInput input)
        {
            return Search(input);
        }
    }
}
