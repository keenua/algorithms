using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public interface ISolution
    {
        string Name { get; }
        bool TestFilesExist { get; }
        void CreateTest();
        void Test();
    }
}
