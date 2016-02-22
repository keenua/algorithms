using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class UnsortedSequence : Solution<List<int>, List<int>>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new List<int> {1, 2}, Output = new List<int> { 2, 1 } },
                new TestInput { Input = new List<int> {1, 2, 3}, Output = new List<int> { 1, 3, 2 } },
                new TestInput { Input = new List<int> {7,2,2}, Output = new List<int> { 2, 7, 2 } },
                new TestInput { Input = new List<int> {1000}, Output = new List<int> {  } },
                new TestInput { Input = new List<int> {1,1}, Output = new List<int> {  } },
                new TestInput { Input = new List<int> {1,2,4,3}, Output = new List<int>{1,2,4,3} },
                new TestInput { Input = new List<int> {2,8,5,1,10,5,9,9,3,10,5,6,6,2,8,2,10}, Output = new List<int> {1, 2, 2, 2, 3, 5, 5, 5, 6, 6, 8, 8, 9, 10, 9, 10, 10 } },
            };
        }

        private List<int> MergeSort(List<int> input)
        {
            if (input.Count < 2) return input;

            int middle = input.Count / 2;

            var left = MergeSort(input.GetRange(0, middle));
            var right = MergeSort(input.GetRange(middle, input.Count - middle));

            var leftPtr = 0;
            var rightPtr = 0;

            var result = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                if (leftPtr == left.Count)
                    result.Add(right[rightPtr++]);
                else if (rightPtr == right.Count)
                    result.Add(left[leftPtr++]);
                else if (left[leftPtr] < right[rightPtr])
                    result.Add(left[leftPtr++]);
                else result.Add(right[rightPtr++]);
            }

            return result;
        }

        public override List<int> Do(List<int> input)
        {
            input = MergeSort(input);

            var toSwap = -1;
            var last = input.Count - 1;

            for (int i = input.Count - 2; i >= 0; i--)
            {
                if (input[input.Count - 1] == input[i]) last = i;

                if (input[i] != input[input.Count - 1])
                {
                    toSwap = i;
                    break;
                }
            }

            if (toSwap == -1) return new List<int>();

            var temp = input[last];
            input[last] = input[toSwap];
            input[toSwap] = temp;

            return input;
        }
    }
}
