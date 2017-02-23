using NUnit.Framework;
using Program.IOManagement;

namespace Tests.ExerciseTests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void TestMethod1()
        {
            var loader = new Loader();
            Assert.That(loader.Load(), Is.EqualTo("content"));
        }
    }
}
