using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class SquaredSubsetsInput
    {
        public int N { get; set; }
        public int[] X { get; set; }
        public int[] Y { get; set; }
    }


    public class SquaredSubsets : Solution<SquaredSubsetsInput, long>
    {
        class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Index { get; set; }
        }

        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new SquaredSubsetsInput { X = new[] {-5,0,5 }, Y = new [] { 0, 0, 0 }, N = 5 }, Output = 5 },
                new TestInput { Input = new SquaredSubsetsInput { X = new[] {-5,0,5 }, Y = new [] { 0, 0, 0 }, N = 10 }, Output = 5 },
                new TestInput { Input = new SquaredSubsetsInput { X = new[] {-1,-1,-1,0,1,1,1}, Y = new [] {-1,0,1,1,-1,0,1}, N = 100000000 }, Output = 21 },
                new TestInput { Input = new SquaredSubsetsInput { X = new[] {5,3,6,2,1,6,4,4,7,-1,-4,-7,2,-2,0}, Y = new [] {0,-1,8,-5,2,5,-8,8,-6,4,3,2,7,3,5}, N = 5 }, Output = 66 },
                new TestInput { Input = new SquaredSubsetsInput { X = new[] {-1, 0}, Y = new [] {0,0}, N = 1 }, Output = 3 }
            };
        }
        
        public override long Do(SquaredSubsetsInput input)
        {
            var size = input.N * 2;

            var xx = new List<int>();
            var yy = new List<int>();

            for (int i = 0; i < input.X.Length; i++)
            {
                input.X[i] *= 2;
                var x = input.X[i];

                xx.Add(x);
                xx.Add(x - size - 1);

                input.Y[i] *= 2;
                var y = input.Y[i];

                yy.Add(y);
                yy.Add(y - size - 1);
            }

            var result = new HashSet<long>();

            for (int i = 0; i < xx.Count; i++)
                for (int j = 0; j < yy.Count; j++)
                {
                    var x = xx[i];
                    var y = yy[j];

                    long mask = 0;

                    for (int k = 0; k < input.X.Length; k++)
                        if (input.X[k] >= x && input.X[k] <= x + size && input.Y[k] >= y && input.Y[k] <= y + size)
                            mask |= 1L << k;

                    if (mask != 0) result.Add(mask);
                }

            return result.Count;
        }
    }
}
