using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.DynamicProgramming;
using Sandbox.Sorting;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = new SquaredSubsets();
            n.CreateTest();

            var solutions = new ISolution[]
            {
                // Dynamic programming

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
                new Jewelry(),
                new QuickSums(),
                new ShortPalindromes(),
                new StripePainter(),
                new StarAdventure(),
                new MiniPaint(),

                // Sorting
                
                new BubbleSort(),
                new InsertionSort(),
                new MergeSort(),
                new HeapSort(),
                new QuickSort(),
                new RadixSort(),
                new GumiAndSongs(),
                new FoxAndMp3(),
                new UnsortedSequence(),
                new TheEncryption(),
                new SquaredSubsets()
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
