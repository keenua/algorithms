using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class ChessMetricInput
    {
        public int Size { get; set; }
        public int[] Start { get; set; }
        public int[] Finish { get; set; }
        public int NumMoves { get; set; }
    }

    public class ChessMetric : Solution<ChessMetricInput, long>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new ChessMetricInput { Size = 3, Start = new[] { 0, 0 }, Finish = new[] { 1, 0 }, NumMoves = 1 }, Output = 1 },
                new TestInput { Input = new ChessMetricInput { Size = 3, Start = new[] { 0, 0 }, Finish = new[] { 1, 2 }, NumMoves = 1 }, Output = 1 },
                new TestInput { Input = new ChessMetricInput { Size = 3, Start = new[] { 0, 0 }, Finish = new[] { 2, 2 }, NumMoves = 1 }, Output = 0 },
                new TestInput { Input = new ChessMetricInput { Size = 3, Start = new[] { 0, 0 }, Finish = new[] { 0, 0 }, NumMoves = 2 }, Output = 5 },
                new TestInput { Input = new ChessMetricInput { Size = 100, Start = new[] { 0, 0 }, Finish = new[] { 0, 99 }, NumMoves = 50 }, Output = 243097320072600 }
            };
        }

        public override long Do(ChessMetricInput input)
        {
            var dpPrev = new long[input.Size, input.Size];
            var dpCurrent = new long[input.Size, input.Size];

            dpPrev[input.Start[0], input.Start[1]] = 1;

            var moves = new List<int[]>
            {
                new int[] { 0, -1 },
                new int[] { 0, 1 },
                new int[] { -1, 0 },
                new int[] { 1, 0 },
                new int[] { 1, -1 },
                new int[] { -1, 1 },
                new int[] { -1, -1 },
                new int[] { 1, 1 },
                new int[] { 2, 1 },
                new int[] { 2, -1 },
                new int[] { -2, 1 },
                new int[] { -2, -1 },
                new int[] { 1, 2 },
                new int[] { -1, 2 },
                new int[] { 1, -2 },
                new int[] { -1, -2 }
            };

            for (int turn = 0; turn < input.NumMoves; turn++)
            {
                for (int i = 0; i < input.Size; i++)
                    for (int j = 0; j < input.Size; j++)
                    {
                        foreach (var m in moves)
                        {
                            var newI = i + m[0];
                            var newJ = j + m[1];

                            if (newI < 0 || newI >= input.Size || newJ < 0 || newJ >= input.Size) continue;

                            dpCurrent[newI, newJ] += dpPrev[i, j];
                        }
                    }

                dpPrev = dpCurrent;
                dpCurrent = new long[input.Size, input.Size];
            }

            return dpPrev[input.Finish[0], input.Finish[1]];
        }
    }
}
