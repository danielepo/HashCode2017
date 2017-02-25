using System;
using System.Collections.Generic;
using System.Linq;
using Program.IOManagement;

namespace Program
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }

    public class RandomSolver
    {
        private readonly ILoader _loader;
        private readonly IGenerator _generator;

        public RandomSolver(ILoader loader, IGenerator generator)
        {
            _loader = loader;
            _generator = generator;
        }

        public void Run()
        {
            var input = _loader.Load();
            var servers = new List<Server>();
            var sum = input.Requests.Select(r => r.NumberOfRequests).Sum() / input.NumberOfRequests;
            var scale = sum / 10;
            scale = scale != 0 ? scale : 1;
            for (int i = 0; i < input.NumberOfCacheServers; i++)
            {
                servers.Add(new Server(input.CapacityOfCacheServer) { ID = i });
            }
            var requests = Map(input);
            var byId = requests.ToDictionary(r => r.ID, r => r);
            var exploded = Explode(requests, scale);
            Serve(exploded, byId, servers);
            Dictionary<int, List<Video>> dict = servers.ToDictionary(x => x.ID, x => x.Videos);
            string content = _generator.Convert(dict);
            _generator.Generate(content);
        }

        private void Serve(List<int> exploded, Dictionary<int, FullRequest> byId, List<Server> servers)
        {
            foreach (var id in exploded)
            {
                var request = byId[id];
                request.EnsureServers(servers);
                request.Serve();
            }
        }

        private List<int> Explode(List<FullRequest> requests, int scale)
        {
            return requests.SelectMany(r => Enumerable.Repeat(r.ID, r.NumberOfRequests / scale)).ToList().Shuffle().ToList();
        }

        private List<FullRequest> Map(Data input)
        {
            var videos = input.Videos.ToDictionary(x => x.ID, x => x);
            var endpoints = input.Endpoint.ToDictionary(x => x.ID, x => x);
            return input.Requests.Select((r, i) => { return new FullRequest() { ID = i, Video = videos[r.VideoID], EndPoint = new FullEndPoint(endpoints[r.EndPointID]), NumberOfRequests = r.NumberOfRequests }; }).ToList();
        }
    }

    public class FullEndPoint
    {
        private List<Server> Servers = new List<Server>();

        public List<ConnectedServer> ConnectedServers { get; private set; }
        public int LatencyDataCenter { get; private set; }
        public int ID { get; private set; }

        public FullEndPoint(EndPoint endPoint)
        {
            ID = endPoint.ID;
            LatencyDataCenter = endPoint.LatencyDataCenter;
            ConnectedServers = endPoint.ConnectedServers;
            ConnectedServers.Sort((s1, s2) => s1.LatencyCache - s2.LatencyCache);
        }

        internal void EnsureServers(List<Server> servers)
        {
            if (Servers.Count > 0)
                return;
            Servers.AddRange(ConnectedServers.Select(s => servers[s.CacheServerID]));
        }

        internal void Serve(Video video)
        {
            if (Servers.Any(s => s.HasVideo(video.ID)))
                return;
            if (Servers.Count == 0)
                return;
            var toAdd = Servers.FirstOrDefault(s => s.CanHold(video));
            if (toAdd == null)
                toAdd = Servers[0];
            toAdd.Add(video);
        }
    }

    internal class Server
    {
        private int capacity;

        public int ID { get; internal set; }
        public List<Video> Videos { get; internal set; }

        public Server(int capacity)
        {
            this.capacity = capacity;
            Videos = new List<Video>();
        }

        internal bool HasVideo(int id)
        {
            return Videos.Any(v => v.ID == id);
        }

        internal void Add(Video video)
        {
            capacity -= video.Size;
            Videos.Add(video);
            while (capacity < 0)
            {
                var toRemove = Videos[0];
                Videos.RemoveAt(0);
                capacity += toRemove.Size;
            }
        }

        internal bool CanHold(Video video)
        {
            return capacity >= video.Size;
        }
    }

    internal class FullRequest
    {
        public int ID { get; internal set; }
        public int NumberOfRequests { get; internal set; }
        public FullEndPoint EndPoint { get; internal set; }
        public Video Video { get; internal set; }

        internal void EnsureServers(List<Server> servers)
        {
            EndPoint.EnsureServers(servers);
        }

        internal void Serve()
        {
            EndPoint.Serve(Video);
        }
    }
}