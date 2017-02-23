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
            _generator.Convert(servers.ToDictionary(x => Map(x), x => GetVideoForServer(x, input.CapacityOfCacheServer, input.Videos.ToDictionary(y => y.ID))));
        }

        private List<Video> GetVideoForServer(Server server, int maxWeight, Dictionary<int, Video> videos)
        {
            List<Item> videoForKnapsack = GetVideoForKnapsack(server);

            var kn = new Knapsack(videoForKnapsack, maxWeight);
            kn.Run();
            return kn.Print().Select(x => videos[x.Id]).ToList();
        }

        private ConnectedServer Map(Server server)
        {
            throw new NotImplementedException();
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

            Dictionary<Video, int> quasiItem = dict.ToDictionary(x => x.Key, x => x.Value.Select(req => req.DeltaLatency * req.Count).Sum());

            return quasiItem.Select(x => new Item() { Id = x.Key.ID, v = x.Value, w = x.Key.Size }).ToList();
        }

        private List<Server> Map(Data input)
        {
            throw new NotImplementedException();
        }
    }

    public class Server
    {
        public List<EndPoint2> endPoints;
    }

    public class EndPoint2
    {
        public List<Request2> requests;
    }

    public class Request2
    {
        public Video Video;
        public int DeltaLatency;
        public int Count;
    }
}