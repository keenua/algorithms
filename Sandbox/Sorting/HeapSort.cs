using Sandbox.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class HeapSort : SortingSolution
    {
        public override List<int> Do(List<int> input)
        {
            var heap = new PriorityQueue<int>();

            foreach (var i in input) heap.Enqueue(i, i);

            var result = new List<int>();

            while (heap.NumItems > 0) result.Add(heap.Dequeue());

            return result;
        }
    }
}
