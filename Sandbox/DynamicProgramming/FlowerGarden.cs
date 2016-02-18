using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class FlowerGardenInput
    {
        public int[] Height { get; set; }
        public int[] Bloom { get; set; }
        public int[] Wilt { get; set; }
    }



    public class FlowerGarden : Solution<FlowerGardenInput, List<int>>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput
                {
                    Input = new FlowerGardenInput
                        {
                            Height = new int[] {5,4,3,2,1},
                            Bloom = new int[] {1,1,1,1,1},
                            Wilt = new int[] {365,365,365,365,365 }
                        },
                    Output = new List<int> { 1,  2,  3,  4,  5 }
                },
                new TestInput
                {
                    Input = new FlowerGardenInput
                        {
                            Height = new int[] {5,4,3,2,1},
                            Bloom = new int[] {1,5,10,15,20},
                            Wilt = new int[] {4,9,14,19,24}
                        },
                    Output = new List<int> { 5,  4,  3,  2,  1 }
                },
                new TestInput
                {
                    Input = new FlowerGardenInput
                        {
                            Height = new int[] {5,4,3,2,1},
                            Bloom = new int[] {1,5,10,15,20},
                            Wilt = new int[] {5,10,15,20,25}
                        },
                    Output = new List<int> { 1,  2,  3,  4,  5 }
                },
                new TestInput
                {
                    Input = new FlowerGardenInput
                        {
                            Height = new int[] {5,4,3,2,1},
                            Bloom = new int[] {1,5,10,15,20},
                            Wilt = new int[] {5,10,14,20,25}
                        },
                    Output = new List<int> { 3,  4,  5,  1,  2 }
                },
                new TestInput
                {
                    Input = new FlowerGardenInput
                        {
                            Height = new int[] {1,2,3,4,5,6},
                            Bloom = new int[] {1,3,1,3,1,3},
                            Wilt = new int[] {2,4,2,4,2,4}
                        },
                    Output = new List<int> { 2,  4,  6,  1,  3,  5 }
                },
                new TestInput
                {
                    Input = new FlowerGardenInput
                        {
                            Height = new int[] {3,2,5,4},
                            Bloom = new int[] {1,2,11,10},
                            Wilt = new int[] {4,3,12,13}
                        },
                    Output = new List<int> { 4,  5,  2,  3 }
                }
            };
        }

        private bool CanBePutInFront(FlowerGardenInput input, int front, int back)
        {
            return input.Height[front] < input.Height[back] || input.Bloom[front] > input.Wilt[back] || input.Wilt[front] < input.Bloom[back];
        }

        public override List<int> Do(FlowerGardenInput input)
        {
            var result = new List<int>();

            bool[,] canBePutInFront = new bool[input.Height.Length, input.Height.Length];

            for (int i = 0; i < input.Height.Length; i++)
                for (int j = 0; j < input.Height.Length; j++)
                {
                    if (i == j) continue;

                    canBePutInFront[i, j] = CanBePutInFront(input, i, j);
                }

            var used = new bool[input.Height.Length];
            
            for (int i = 0; i < input.Height.Length; i++)
            {
                var max = -1;

                for (int j = 0; j < input.Height.Length; j++)
                {
                    if (!used[j])
                    {
                        bool blocked = false;

                        for (int k = 0; k < input.Height.Length; k++)
                        {
                            if (j == k || used[k]) continue;

                            blocked |= !canBePutInFront[j, k];

                            if (blocked) break;
                        }

                        if (!blocked && (max == -1 || input.Height[max] < input.Height[j]))
                            max = j;
                    } 
                }

                used[max] = true;
                result.Add(input.Height[max]);
            }

            return result;
        }
    }
}
