using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class MergeSort : SortingSolution
    {
        public override List<int> Do(List<int> input)
        {
            if (input.Count < 2) return input;

            var middle = input.Count / 2;

            var left = Do(input.GetRange(0, middle));
            var right = Do(input.GetRange(middle, input.Count - middle));

            var result = new List<int>();

            var leftPtr = 0;
            var rightPtr = 0;

            while (result.Count != input.Count)
            {
                if (leftPtr == left.Count)
                {
                    result.Add(right[rightPtr++]);
                }
                else if (rightPtr == right.Count)
                {
                    result.Add(left[leftPtr++]);
                }
                else if (right[rightPtr] < left[leftPtr])
                {
                    result.Add(right[rightPtr++]);
                }
                else result.Add(left[leftPtr++]);
            }

            return result;
        }
    }
}
