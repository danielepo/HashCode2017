using InputFileLoader;
using NUnit.Framework;

namespace Tests.UnitTests
{
    [TestFixture]
    public class LoaderTests
    {
        [Test]
        public void TestMethod1()
        {
            var loader = new Loader();
            Assert.That(loader.Load(), Is.EqualTo("content"));
        }
    }
}
