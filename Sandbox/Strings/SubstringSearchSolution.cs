using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Strings
{
    public class SubstringSearchSolutionInput
    {
        public string Str { get; set; }
        public string Pattern { get; set; }
    }


    public abstract class SubstringSearchSolution : Solution<SubstringSearchSolutionInput, List<int>>
    {
        protected override List<TestInput> GetTests()
        {
            return new List<TestInput>
            {
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "DDE", Pattern = "DDE" }, Output = new List<int> { 0 } },
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "DDEF", Pattern = "DDE" }, Output = new List<int> { 0 } },
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "DDE", Pattern = "DDEF" }, Output = new List<int> { } },
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "ABSSvd SVB", Pattern = "SS" }, Output = new List<int> { 2 } },
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "ab baab", Pattern = "ab" }, Output = new List<int> { 0, 5 } },
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "ab baab", Pattern = "fa" }, Output = new List<int> { } },
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "ab baab", Pattern = "aab" }, Output = new List<int> { 4 } },
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "aaaaaaaaa", Pattern = "aa" }, Output = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 } },
                new TestInput { Input = new SubstringSearchSolutionInput { Str = "aaaaaaaaa", Pattern = "aaa" }, Output = new List<int> { 0, 1, 2, 3, 4, 5, 6 } }
            };
        }
    }
}
