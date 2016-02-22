using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class RadixSort : SortingSolution
    {
        const int radixBase = 2;

        public override List<int> Do(List<int> input)
        {
            bool finished = false;

            var multiplier = 1;

            var buckets = new List<Queue<int>>();

            for (int i = 0; i < radixBase; i++)
                buckets.Add(new Queue<int>());

            while (!finished)
            {
                finished = true;

                foreach (var i in input)
                {
                    var bucket = i / multiplier;

                    if (bucket != 0) finished = false;

                    bucket %= radixBase;
                    buckets[bucket].Enqueue(i);
                }

                multiplier *= radixBase;

                input.Clear();

                foreach (var b in buckets)
                    while (b.Any()) input.Add(b.Dequeue());
            }

            return input;
        }
    }
}
