using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class QuickSort : SortingSolution
    {
        public override List<int> Do(List<int> input)
        {
            if (input.Count < 2) return input;

            var middle = input[input.Count / 2];

            var left = new List<int>();
            var right = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                if (i == input.Count / 2) continue;

                if (input[i] < middle) left.Add(input[i]);
                else right.Add(input[i]);
            }

            var result = new List<int>();

            result.AddRange(Do(left));
            result.Add(middle);
            result.AddRange(Do(right));

            return result;
        }
    }
}
