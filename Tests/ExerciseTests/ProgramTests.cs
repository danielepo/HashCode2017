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
    public class GeneratorTests
    {
        class Convert
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
            [Test]
            public void TestMethod4()
            {
                var generator = new Generator();
                var result = new Dictionary<CacheServer, List<Video>>
            {
                {new CacheServer {ID = 1}, new List<Video> {new Video {ID = 10}}}
            };
                var converted = generator.Convert(result);
                Assert.That(converted.GetRow(1), Is.EqualTo("1"));
                Assert.That(converted.GetRow(2), Is.EqualTo("1 10"));
            }
            [Test]
            public void TestMethod5()
            {
                var generator = new Generator();
                var result = new Dictionary<CacheServer, List<Video>>
            {
                {new CacheServer {ID = 1}, new List<Video> {new Video {ID = 10 } }},
                {new CacheServer {ID = 2}, new List<Video> {new Video {ID = 22 } }}
            };
                var converted = generator.Convert(result);
                Assert.That(converted.GetRow(1), Is.EqualTo("2"));
                Assert.That(converted.GetRow(2), Is.EqualTo("1 10"));
                Assert.That(converted.GetRow(3), Is.EqualTo("2 22"));
            }

            [Test]
            public void TestMethod6()
            {
                var generator = new Generator();
                var result = new Dictionary<CacheServer, List<Video>>
            {
                {new CacheServer {ID = 1}, new List<Video>
                {
                    new Video {ID = 10},
                    new Video {ID = 25},
                    new Video {ID = 35},
                    new Video {ID = 47}
                }}
            };
                var converted = generator.Convert(result);
                Assert.That(converted.GetRow(1), Is.EqualTo("1"));
                Assert.That(converted.GetRow(2), Is.EqualTo("1 10 25 35 47"));
            }
            [Test]
            public void TestMethod7()
            {
                var generator = new Generator();
                var result = new Dictionary<CacheServer, List<Video>>
            {
                {
                    new CacheServer {ID = 1}, new List<Video>
                    {
                        new Video {ID = 10},
                        new Video {ID = 25},
                        new Video {ID = 35},
                        new Video {ID = 47}
                    }
                },
                {
                    new CacheServer {ID = 3}, new List<Video>
                    {
                        new Video {ID = 120},
                        new Video {ID = 225},
                        new Video {ID = 325},
                        new Video {ID = 427}
                    }
                },
                {
                    new CacheServer {ID = 700}, new List<Video>
                    {
                        new Video {ID = 35},
                        new Video {ID = 47}
                    }
                }
            };
                var converted = generator.Convert(result);
                Assert.That(converted.GetRow(1), Is.EqualTo("3"));
                Assert.That(converted.GetRow(2), Is.EqualTo("1 10 25 35 47"));
                Assert.That(converted.GetRow(3), Is.EqualTo("3 120 225 325 427"));
                Assert.That(converted.GetRow(4), Is.EqualTo("700 35 47"));
            }
        }

    }
}
