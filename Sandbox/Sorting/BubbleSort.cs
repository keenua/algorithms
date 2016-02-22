using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class BubbleSort : SortingSolution
    {
        public override List<int> Do(List<int> input)
        {
            for (int i = 0; i < input.Count; i++)
                for (int j = 0; j < input.Count - 1; j++)
                    if (input[j] > input[j + 1])
                    {
                        var tmp = input[j + 1];
                        input[j + 1] = input[j];
                        input[j] = tmp;
                    }

            return input;
        }
    }
}
