using System;
using System.Collections.Generic;
using System.Linq;
using Program.IOManagement;

namespace Program
{
    public class Solver
    {
        private readonly ILoader _loader;
        private readonly IGenerator _generator;

        public Solver(ILoader loader, IGenerator generator)
        {
            _loader = loader;
            _generator = generator;
        }

        public void Run()
        {
            var input = _loader.Load();
            List<Server> servers = Map(input);
            string content = _generator.Convert(servers.ToDictionary(x => x.ID, x => GetVideoForServer(x, input.CapacityOfCacheServer, input.Videos.ToDictionary(y => y.ID))));
            _generator.Generate(content);
        }

        public List<Server> Map(Data input)
        {
            List<Server> servers = new List<Server>();
            var videos = input.Videos.ToDictionary(x => x.ID, x => x);
            foreach (EndPoint ep in input.Endpoint)
            {
                foreach (ConnectedServer cs in ep.ConnectedServers)
                {
                    if (!servers.Any(x => x.ID == cs.CacheServerID))
                    {
                        servers.Add(new Server { ID = cs.CacheServerID, Latency = cs.LatencyCache });
                    }
                }
            }

            foreach (Server ser in servers)
            {
                IEnumerable<EndPoint> endPointCheHannoQuestoServer = input.Endpoint.Where(x => x.ConnectedServers.Any(y => y.CacheServerID == ser.ID));
                foreach (EndPoint ep in endPointCheHannoQuestoServer)
                {
                    ser.endPoints.Add(new EndPoint2 { ID = ep.ID, LatencyDataCenter = ep.LatencyDataCenter });
                }

                foreach (EndPoint2 ep2 in ser.endPoints)
                {
                    var richiesteCheVannoSuQuestoEp = input.Requests.Where(x => x.EndPointID == ep2.ID);
                    foreach (Request req in richiesteCheVannoSuQuestoEp)
                    {
                        var req2 = new Request2();
                        req2.Count = req.NumberOfRequests;
                        req2.Video = videos[req.VideoID];
                        req2.DeltaLatency = ep2.LatencyDataCenter - ser.Latency;

                        ep2.requests.Add(req2);
                    }
                }
            }

            return servers;
        }

        private List<Video> GetVideoForServer(Server server, int maxWeight, Dictionary<int, Video> videos)
        {
            List<Item> videoForKnapsack = GetVideoForKnapsack(server);

            var kn = new Knapsack(videoForKnapsack, maxWeight);
            kn.Run();
            return kn.Print().Select(x => videos[x.Id]).ToList();
        }

        private List<Item> GetVideoForKnapsack(Server server)
        {
            IDictionary<Video, List<Request2>> dict = new Dictionary<Video, List<Request2>>();
            foreach (Request2 request2 in server.endPoints.SelectMany(x => x.requests))
            {
                Video video = request2.Video;
                if (!dict.ContainsKey(video))
                {
                    dict[video] = new List<Request2>();
                }
                dict[video].Add(request2);
            }

            Dictionary<Video, int> quasiItem = dict.ToDictionary(x => x.Key, x => x.Value.Select(req => req.DeltaLatency * req.Count)
                .Sum());

            return quasiItem.Select(x => new Item() { Id = x.Key.ID, v = x.Value, w = x.Key.Size }).ToList();
        }

        public class Server
        {
            public int ID;
            public int Latency;
            public List<EndPoint2> endPoints = new List<EndPoint2>();
        }

        public class EndPoint2
        {
            public int ID;
            public int LatencyDataCenter;
            public List<Request2> requests = new List<Request2>();
        }

        public class Request2
        {
            public Video Video;
            public int DeltaLatency;
            public int Count;
        }
    }
}