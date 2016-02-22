using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public abstract class SortingSolution : Solution<List<int>, List<int>>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new List<int> { 2, 1, 8, 15, 2 }, Output = new List<int> { 1, 2, 2, 8, 15 } },
                new TestInput { Input = new List<int> { 14 }, Output = new List<int> { 14 } },
                new TestInput { Input = new List<int> {18,  6,  9,  1,  4, 15, 12,  5,  6,  7, 11}, Output = new List<int> { 1,  4,  5,  6,  6,  7,  9, 11, 12, 15, 18 } },
                new TestInput { Input = new List<int> { }, Output = new List<int> { } }
            };
        }
    }
}
