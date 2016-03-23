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
        private string TestFileInput { get { return $@"Tests\{Name}_input.txt"; } }
        private string TestFileOutput { get { return $@"Tests\{Name}_output.txt"; } }

        public bool TestFilesExist { get { return File.Exists(TestFileInput) && File.Exists(TestFileOutput); } }

        protected Solution()
        {
            Name = GetType().Name;
        }
        
        public abstract TOutput Do(TInput input);
        protected virtual List<TestInput> GetTests() { return new List<TestInput>(); }

        private void CreateFile<T>(List<T> values, string path)
        {
            var dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

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
            CreateFile(input, TestFileInput);
            CreateFile(output, TestFileOutput);
        }

        public void CreateTest()
        {
            var tests = GetTests();

            CreateTest(tests.Select(x => x.Input).ToList(), tests.Select(x => x.Output).ToList());
        }

        protected bool CompareList<T>(TOutput actual, TOutput expected)
        {
            if (actual is List<T>)
            {
                var aL = actual as List<T>;
                var eL = expected as List<T>;

                if (aL == null || eL == null) return aL == eL;

                if (aL.Count != eL.Count) return false;

                return !aL.Where((x, i) => !Equals(eL[i], x)).Any();
            }

            return false;
        }

        protected virtual bool Compare(TOutput actual, TOutput expected)
        {
            if (actual is List<int>) return CompareList<int>(actual, expected);
            if (actual is List<string>) return CompareList<string>(actual, expected);

            return Equals(actual, expected);
        }

        public void Test()
        {
            var input = ReadFile<TInput>(TestFileInput);
            var expectedOutput = ReadFile<TOutput>(TestFileOutput);

            for (int i = 0; i < input.Count; i++)
            {
                var output = Do(input[i]);
                
                if (!Compare(expectedOutput[i], output))
                {
                    Debugger.Break();
                    Debug.Assert(false);
                }
            }
        }
    }
}
