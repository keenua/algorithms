using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public static class Extensions
    {
        public static int[,] ToGraph(this string str)
        {
            var split = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var result = new int[ split.Length, split[0].Split(' ').Length];

            for (int i = 0; i < split.Length; i++)
            {
                var values = split[i].Split(' ').Select(int.Parse).ToArray();

                for (int j = 0; j < values.Length; j++)
                    result[i, j] = values[j];
            }

            return result;
        }

        public static List<int> ToPath(this int[] previous)
        {
            if (!previous.Any(x => x != -1)) return new List<int>();

            int current = previous.Length - 1;

            List<int> result = new List<int>();
            result.Add(current);

            while (current != 0)
            {
                result.Add(previous[current]);

                current = previous[current];
            }

            result.Reverse();
            return result;
        }
    }
}
