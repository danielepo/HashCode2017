using System.Collections.Generic;
using NUnit.Framework;
using Program;
using Program.IOManagement;

namespace Tests.ExerciseTests
{
    internal static class Extensions
    {
        public static string GetRow(this string input, int i)
        {
            return input.Split('\n')[i - 1];
        }
    }
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void TestMethod1()
        {
            var generator = new Generator();
            var result = new Dictionary<CacheServer, List<Video>>();
            Assert.That(generator.Convert(result).GetRow(1), Is.EqualTo("0"));
        }
        [Test]
        public void TestMethod2()
        {
            var generator = new Generator();
            var result = new Dictionary<CacheServer, List<Video>>
            {
                [new CacheServer()] = new List<Video>()
            };
            Assert.That(generator.Convert(result).GetRow(1), Is.EqualTo("1"));
        }

        [Test]
        public void TestMethod3()
        {
            var generator = new Generator();
            var result = new Dictionary<CacheServer, List<Video>>
            {
                [new CacheServer()] = new List<Video>(),
                [new CacheServer()] = new List<Video>()
            };
            Assert.That(generator.Convert(result).GetRow(1), Is.EqualTo("2"));
        }
    }
}
