using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Sorting
{
    public class FoxAndMp3 : Solution<int, List<string>>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = 10, Output = new List<string> {"1.mp3",
"10.mp3",
"2.mp3",
"3.mp3",
"4.mp3",
"5.mp3",
"6.mp3",
"7.mp3",
"8.mp3",
"9.mp3" } },
                new TestInput {Input = 16, Output = new List<string> {"1.mp3",
"10.mp3",
"11.mp3",
"12.mp3",
"13.mp3",
"14.mp3",
"15.mp3",
"16.mp3",
"2.mp3",
"3.mp3",
"4.mp3",
"5.mp3",
"6.mp3",
"7.mp3",
"8.mp3",
"9.mp3" } },
new TestInput {Input = 32, Output = new List<string>
{"1.mp3",
"10.mp3",
"11.mp3",
"12.mp3",
"13.mp3",
"14.mp3",
"15.mp3",
"16.mp3",
"17.mp3",
"18.mp3",
"19.mp3",
"2.mp3",
"20.mp3",
"21.mp3",
"22.mp3",
"23.mp3",
"24.mp3",
"25.mp3",
"26.mp3",
"27.mp3",
"28.mp3",
"29.mp3",
"3.mp3",
"30.mp3",
"31.mp3",
"32.mp3",
"4.mp3",
"5.mp3",
"6.mp3",
"7.mp3",
"8.mp3",
"9.mp3" } },
new TestInput {Input = 109, Output = new List<string>
{"1.mp3",
"10.mp3",
"100.mp3",
"101.mp3",
"102.mp3",
"103.mp3",
"104.mp3",
"105.mp3",
"106.mp3",
"107.mp3",
"108.mp3",
"109.mp3",
"11.mp3",
"12.mp3",
"13.mp3",
"14.mp3",
"15.mp3",
"16.mp3",
"17.mp3",
"18.mp3",
"19.mp3",
"2.mp3",
"20.mp3",
"21.mp3",
"22.mp3",
"23.mp3",
"24.mp3",
"25.mp3",
"26.mp3",
"27.mp3",
"28.mp3",
"29.mp3",
"3.mp3",
"30.mp3",
"31.mp3",
"32.mp3",
"33.mp3",
"34.mp3",
"35.mp3",
"36.mp3",
"37.mp3",
"38.mp3",
"39.mp3",
"4.mp3",
"40.mp3",
"41.mp3",
"42.mp3",
"43.mp3",
"44.mp3",
"45.mp3" } },
new TestInput {Input = 100000009, Output = new List<string> {
                "1.mp3",
"10.mp3",
"100.mp3",
"1000.mp3",
"10000.mp3",
"100000.mp3",
"1000000.mp3",
"10000000.mp3",
"100000000.mp3",
"100000001.mp3",
"100000002.mp3",
"100000003.mp3",
"100000004.mp3",
"100000005.mp3",
"100000006.mp3",
"100000007.mp3",
"100000008.mp3",
"100000009.mp3",
"10000001.mp3",
"10000002.mp3",
"10000003.mp3",
"10000004.mp3",
"10000005.mp3",
"10000006.mp3",
"10000007.mp3",
"10000008.mp3",
"10000009.mp3",
"1000001.mp3",
"10000010.mp3",
"10000011.mp3",
"10000012.mp3",
"10000013.mp3",
"10000014.mp3",
"10000015.mp3",
"10000016.mp3",
"10000017.mp3",
"10000018.mp3",
"10000019.mp3",
"1000002.mp3",
"10000020.mp3",
"10000021.mp3",
"10000022.mp3",
"10000023.mp3",
"10000024.mp3",
"10000025.mp3",
"10000026.mp3",
"10000027.mp3",
"10000028.mp3",
"10000029.mp3",
"1000003.mp3" } }
        };
        }

        private List<string> GetFirst(int prefix, int maxNumber, int take)
        {
            var result = new List<string>();

            for (int digit = prefix == 0 ? 1 : 0; digit < Math.Min(maxNumber + 1, 10); digit++)
            {
                var number = prefix * 10 + digit;

                if (prefix * 10 + digit > maxNumber || result.Count >= take) break;

                result.Add(number + ".mp3");

                result.AddRange(GetFirst(number, maxNumber, take - result.Count));
            }

            return result;
        }

        public override List<string> Do(int input)
        {
            return GetFirst(0, input, 50);
        }
    }
}
