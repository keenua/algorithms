using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class InsertionSort : SortingSolution
    {
        public override List<int> Do(List<int> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                var j = i;

                while (j > 0 && input[i] < input[j - 1])
                    j--;

                var tmp = input[i];

                for (int k = i; k > j; k--)
                    input[k] = input[k - 1];

                input[j] = tmp; 
            }

            return input;
        }
    }
}
