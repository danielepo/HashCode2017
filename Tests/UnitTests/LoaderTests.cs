using NUnit.Framework;
using Program;
using Program.IOManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        [Test]
        public void TestMethod2()
        {
            Data expected = new Data();

            expected.NumberOfVideos = 5;
            expected.NumberOfEndpoints = 2;
            expected.NumberOfRequests = 4;
            expected.CapacityOfCacheServer = 100;

            expected.Videos.Add(new Video { ID = 0, Size = 50 });
            expected.Videos.Add(new Video { ID = 1, Size = 50 });
            expected.Videos.Add(new Video { ID = 2, Size = 80 });
            expected.Videos.Add(new Video { ID = 3, Size = 30 });
            expected.Videos.Add(new Video { ID = 4, Size = 110 });

            var ep1 = new EndPoint
            {
                ID = 0,
                LatencyDataCenter = 1000
            };

            ep1.ConnectedServers.Add(new ConnectedServer { CacheServerID = 0, LatencyCache = 100 });
            ep1.ConnectedServers.Add(new ConnectedServer { CacheServerID = 2, LatencyCache = 200 });
            ep1.ConnectedServers.Add(new ConnectedServer { CacheServerID = 1, LatencyCache = 300 });
            expected.Endpoint.Add(ep1);

            var ep2 = new EndPoint
            {
                ID = 1,
                LatencyDataCenter = 500
            };

            expected.Endpoint.Add(ep2);

            expected.Requests.Add(new Request { NumberOfRequests = 1500, VideoID = 3, EndPointID = 0 });
            expected.Requests.Add(new Request { NumberOfRequests = 1000, VideoID = 0, EndPointID = 1 });
            expected.Requests.Add(new Request { NumberOfRequests = 500, VideoID = 4, EndPointID = 0 });
            expected.Requests.Add(new Request { NumberOfRequests = 1000, VideoID = 1, EndPointID = 0 });



            long sizeExpected;


            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, expected);
                sizeExpected = s.Length;
            }


            var load = new LoadFile();

            Data result = load.LoadFrom("C:\\Utenti\\piv\\Desktop\\datas.txt");

            long sizeResult;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, expected);
                sizeResult = s.Length;
            }


            Assert.AreEqual(sizeResult, sizeExpected);

        }



    }
}
