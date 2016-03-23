using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Strings
{
    public class RabinKarpDoubleHashAlgorithm : RabinKarpAlgorithm
    {
        private const ulong B = 256;
        private const ulong M1 = 1000004123;
        private const ulong M2 = 2147483647;

        private static ulong[] Hash(string str, ulong B, ulong[] M, int start = 0, int length = -1)
        {
            var hash = new ulong[M.Length];

            for (int i = 0; i < M.Length; i++)
                hash[i] = Hash(str, B, M[i], start, length);

            return hash;
        }

        private static bool HashEquals(ulong[] hash1, ulong[] hash2)
        {
            if (hash1 == null || hash2 == null || hash1.Length != hash2.Length) return false;

            for (int i = 0; i < hash1.Length; i++)
                if (hash1[i] != hash2[i])
                    return false;

            return true;
        }

        protected override List<int> Search(SubstringSearchSolutionInput input)
        {
            var result = new List<int>();

            if (input.Pattern.Length > input.Str.Length) return result;

            var m = new ulong[] { M1, M2 };

            var patternHash = Hash(input.Pattern, B, m);

            var segmentHash = Hash(input.Str, B, m, 0, input.Pattern.Length);

            if (HashEquals(patternHash, segmentHash))
                result.Add(0);

            var e = new ulong[m.Length];

            for (int i = 0; i < m.Length; i++)
                e[i] = Mod(B, (ulong)(input.Pattern.Length - 1), m[i]);

            for (int i = input.Pattern.Length; i < input.Str.Length; i++)
            {
                for (int j = 0; j < m.Length; j++)
                {
                    segmentHash[j] = Mod(segmentHash[j] - Mod(e[j] * input.Str[i - input.Pattern.Length], m[j]), m[j]);
                    segmentHash[j] = Mod(segmentHash[j] * B, m[j]);
                    segmentHash[j] = Mod(segmentHash[j] + input.Str[i], m[j]);
                }

                if (HashEquals(segmentHash, patternHash))
                    result.Add(i - input.Pattern.Length + 1);
            }

            return result;
        }
    }
}
