using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sandbox
{
    

    public abstract class Solution<TInput, TOutput> : ISolution
    {
        protected struct TestInput
        {
            public TInput Input { get; set; }
            public TOutput Output { get; set; }
        }

        public string Name { get; private set; }

        protected Solution()
        {
            Name = GetType().Name;
        }

        public abstract TOutput Do(TInput input);
        protected virtual List<TestInput> GetTests() { return new List<TestInput>(); }

        private void CreateFile<T>(List<T> values, string path)
        {
            var inputSerializer = new XmlSerializer(typeof(List<T>));

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                inputSerializer.Serialize(stream, values);
        }

        private List<T> ReadFile<T>(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                return (List<T>)serializer.Deserialize(stream);
            }
        }

        public void CreateTest(TInput input, TOutput output)
        {
            CreateTest(new List<TInput> { input }, new List<TOutput> { output });
        }

        public void CreateTest(List<TInput> input, List<TOutput> output)
        {
            CreateFile(input, $"{Name}_input.txt");
            CreateFile(output, $"{Name}_output.txt");
        }

        public void CreateTest()
        {
            var tests = GetTests();

            CreateTest(tests.Select(x => x.Input).ToList(), tests.Select(x => x.Output).ToList());
        }

        protected virtual bool Compare(TOutput actual, TOutput expected)
        {
            if (actual is List<int>)
            {
                var aL = actual as List<int>;
                var eL = expected as List<int>;

                if (aL == null || eL == null) return aL == eL;

                if (aL.Count != eL.Count) return false;

                return !aL.Where((x, i) => eL[i] != x).Any();
            }

            return Equals(actual, expected);
        }

        public void Test()
        {
            var input = ReadFile<TInput>($"{Name}_input.txt");
            var expectedOutput = ReadFile<TOutput>($"{Name}_output.txt");

            for (int i = 0; i < input.Count; i++)
            {
                var output = Do(input[i]);

                Debug.Assert(Compare(expectedOutput[i], output));
            }
        }
    }
}
