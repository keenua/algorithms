using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class CoinsInput
    {
        public int[] Coins { get; set; }
        public int Sum { get; set; }
    }

    public class Coins : Solution<CoinsInput, int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new CoinsInput { Coins = new [] { 1, 3, 5 }, Sum = 11 }, Output = 3 },
                new TestInput { Input = new CoinsInput { Coins = new [] { 1, 3 }, Sum = 11 }, Output = 5 },
                new TestInput { Input = new CoinsInput { Coins = new [] { 2, 9 }, Sum = 8 }, Output = 4 },
                new TestInput { Input = new CoinsInput { Coins = new [] { 4, 9 }, Sum = 5 }, Output = 2147483647 },
            };
        }

        public override int Do(CoinsInput input)
        {
            var min = new int[input.Sum + 1];

            min[0] = 0;
            for (var i = 1; i < input.Sum + 1; i++) min[i] = int.MaxValue;

            for (var i = 1; i <= input.Sum; i++)
                foreach (var coin in input.Coins.Where(x => x <= i))
                {
                    var prev = min[i - coin];

                    if (prev == int.MaxValue || prev + 1 >= min[i]) continue;

                    min[i] = prev + 1;
                }

            return min[input.Sum];
        }
    }
}
