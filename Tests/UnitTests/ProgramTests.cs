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
        public void Generate()
        {
            throw new System.NotImplementedException();
        }
    }

    class LoaderStub : ILoader
    {
        public string Load()
        {
            throw new System.NotImplementedException();
        }
    }
}
