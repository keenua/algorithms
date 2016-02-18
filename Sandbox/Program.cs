using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.DynamicProgramming;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Jewelry n = new Jewelry();
            n.CreateTest();

            var solutions = new ISolution[]
            {
                new Coins(),
                new NonDecreasing(),
                new ShortestPath(),
                new ZigZag(),
                new BadNeighbors(),
                new FlowerGarden(),
                new ApplesGrid(),
                new AvoidRoads(),
                new ChessMetric(),
                new MoneyGraph(),
                new Jewelry()
            };

            foreach (var s in solutions)
            {
                s.Test();
                Console.WriteLine($"{s.Name} passed");
            }

            Console.WriteLine();
        }
    }
}
