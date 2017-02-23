using System.Collections.Generic;
using NUnit.Framework;
using Program;
using Program.IOManagement;
namespace Tests.UnitTests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void TestMethod1()
        {
            var program = new Program.Solver(new LoaderStub(),new GeneratorStub());
            program.Run();
        }

    }

    class GeneratorStub : IGenerator
    {
        public string Convert(Dictionary<ConnectedServer, List<Video>> toDictionary)
        {
            throw new System.NotImplementedException();
        }
    }

    class LoaderStub : ILoader
    {
        public Data Load()
        {
            throw new System.NotImplementedException();
        }
    }
}
