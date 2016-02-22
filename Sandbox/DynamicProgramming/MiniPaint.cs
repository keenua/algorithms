using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicProgramming
{
    public class MiniPaintInput
    {
        public string[] Picture { get; set; }
        public int MaxStrokes { get; set; }
    }

    public class MiniPaint : Solution<MiniPaintInput, int>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new MiniPaintInput { Picture = new []
{"WWWBBBBWWWBBBB"},
MaxStrokes = 1
 }, Output = 6
                },
                new TestInput { Input = new MiniPaintInput { Picture = new []
{
"BBBBBBBBBBBBBBB",
"WWWWWWWWWWWWWWW",
"WWWWWWWWWWWWWWW",
"WWWWWBBBBBWWWWW"},
MaxStrokes = 6
 }, Output = 0
                },
                new TestInput { Input = new MiniPaintInput { Picture = new []
{
"BBBBBBBBBBBBBBB",
"WWWWWWWWWWWWWWW",
"WWWWWWWWWWWWWWW",
"WWWWWBBBBBWWWWW"},
MaxStrokes = 4
 }, Output = 5
                },
                new TestInput { Input = new MiniPaintInput { Picture = new []
{
"BBBBBBBBBBBBBBB",
"WWWWWWWWWWWWWWW",
"WWWWWWWWWWWWWWW",
"WWWWWBBBBBWWWWW"},
MaxStrokes = 0
 }, Output = 60
                },
                new TestInput { Input = new MiniPaintInput { Picture = new []
{
"BWBWBWBWBWBWBWBWBWBWBWBWBWBWBW",
"BWBWBWBWBWBWBWBWBWBWBWBWBWBWBW",
"BWBWBWBWBWBWBWBWBWBWBWBWBWBWBW",
"BWBWBWBWBWBWBWBWBWBWBWBWBWBWBW",
"BWBWBWBWBWBWBWBWBWBWBWBWBWBWBW",
"BWBWBWBWBWBWBWBWBWBWBWBWBWBWBW"},
MaxStrokes = 100
 }, Output = 40
                },
                new TestInput { Input = new MiniPaintInput { Picture = new []
{"B"},
MaxStrokes = 1
 }, Output = 0
                }                
            };
        }

        public override int Do(MiniPaintInput input)
        {
            int lines = input.Picture.Length;
            int cols = input.Picture[0].Length;

            var max = 3000;

            int[,] bad = new int[lines, cols + 1];
            
            for (int line = 0; line < lines; line++)
            {
                var colors = input.Picture[line].Select(x => x == 'B' ? 1 : 2).ToArray();

                var curBad = new int[cols, cols + 1, 3];

                for (int i = cols - 1; i >= 0; i--)
                    for (int strokes = 0; strokes <= cols; strokes++)
                        for (int prevColor = 0; prevColor < 3; prevColor++)
                        {
                            var min = max;
                            
                            for (int color = 0; color < 3; color++)
                            {
                                if (strokes == 0 && color != prevColor) continue;

                                var bads = color != colors[i] ? 1 : 0;
                                var additionalStroke = color != prevColor ? 1 : 0;

                                if (i == cols - 1)
                                {
                                    if (strokes == 0) min = Math.Min(min, bads);
                                    else if (strokes == 1) min = 0;
                                    continue;
                                }

                                min = Math.Min(min, curBad[i + 1, strokes - additionalStroke, color] + bads);
                            }

                            curBad[i, strokes, prevColor] = min;
                        }

                for (int i = 0; i <= cols; i++)
                    bad[line, i] = curBad[0, i, 0];
            }

            var best = new int[lines, input.MaxStrokes + 1];
            for (int line = 0; line < lines; line++)
                for (int s = 0; s <= input.MaxStrokes; s++)
                    best[line, s] = line == 0 ? bad[0, Math.Min(s, cols)] : int.MaxValue;

            for (int line = 1; line < lines; line++)
                for (int s = 0; s <= input.MaxStrokes; s++)
                    for (int i = 0; i <= s; i++)
                        best[line, s] = Math.Min(best[line, s], best[line - 1, s - i] + bad[line, Math.Min(cols, i)]);

            return best[lines - 1, input.MaxStrokes];
        }
    }
}
