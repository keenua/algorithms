using Sandbox.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class GumiAndSongsInput
    {
        public int[] Duration { get; set; }
        public int[] Tone { get; set; }
        public int Time { get; set; }
    }

    public class GumiAndSongs : Solution<GumiAndSongsInput, int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new GumiAndSongsInput { Duration = new int[] {3, 5, 4, 11}, Tone = new int[] {2, 1, 3, 1}, Time = 17 }, Output = 3 },
                new TestInput { Input = new GumiAndSongsInput { Duration = new int[] {100, 200, 300}, Tone = new int[] {1, 2, 3}, Time = 10 }, Output = 0 },
                new TestInput { Input = new GumiAndSongsInput { Duration = new int[] {1, 2, 3, 4}, Tone = new int[] {1, 1, 1, 1}, Time = 100 }, Output = 4 },
                new TestInput { Input = new GumiAndSongsInput { Duration = new int[] {10, 10, 10}, Tone = new int[] {58, 58, 58}, Time = 30 }, Output = 3 },
                new TestInput { Input = new GumiAndSongsInput { Duration = new int[] {8, 11, 7, 15, 9, 16, 7, 9}, Tone = new int[] {3, 8, 5, 4, 2, 7, 4, 1}, Time = 14 }, Output = 1 },
                new TestInput { Input = new GumiAndSongsInput { Duration = new int[] {5611,39996,20200,56574,81643,90131,33486,99568,48112,97168,5600,49145,73590,3979,94614}, Tone = new int[] {2916,53353,64924,86481,44803,61254,99393,5993,40781,2174,67458,74263,69710,40044,80853}, Time = 302606 }, Output = 8 },
            };
        }

        public override int Do(GumiAndSongsInput input)
        {
            var n = input.Duration.Length;

            var sort = new MergeSort();

            var sortedTones = sort.Do(input.Tone.ToList());

            var best = 0;

            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                {
                    var left = input.Time - sortedTones[j] + sortedTones[i];

                    if (left <= 0) continue;

                    int current = 0;

                    var sortedDurations = new PriorityQueue<int>();

                    for (int k = 0; k < n; k++)
                        if (input.Tone[k] >= sortedTones[i] && input.Tone[k] <= sortedTones[j])
                            sortedDurations.Enqueue(input.Duration[k], input.Duration[k]);

                    while (sortedDurations.NumItems > 0)
                    {
                        left -= sortedDurations.Dequeue();
                        if (left >= 0) current++;
                        else break;
                    }

                    best = Math.Max(best, current);
                }

            return best;
        }
    }
}
