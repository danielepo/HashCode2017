using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{


    public sealed class Video {

        public int ID;

        public int Size;

        private bool Equals(Video other)
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            return obj is Video && Equals((Video)obj);
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }

    public class EndPoint {

        public int ID;//0

        public int LatencyDataCenter;

        public List<ConnectedServer> ConnectedServers = new List<ConnectedServer>();
    }


    public class ConnectedServer {

        public int LatencyCache;

        public CacheServer CacheServer;
    }

    public class CacheServer {

        public int ID;//0

       

    }

    public class Request {

        int VideoID;

        int EndPointID;

        int NumberOfRequests;

    }

    public class Data
    {
        public int NumberOfVideos;
        public int NumberOfEndpoints;
        public int NumberOfRequests;
        public int NumberOfCacheServers;
        public int CapacityOfCacheServer;

        public List<Video> Videos = new List<Video>();
        public List<EndPoint> Endpoint = new List<EndPoint>();
    }
}
