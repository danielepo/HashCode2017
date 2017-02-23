using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{


    public class Video {

        public int ID;

        public int Size;



    }

    public class EndPoint {

        public int ID;//0

        public int LatencyDataCenter;

        public List<ConnectedServer> ConnectedServers = new List<ConnectedServer>();
    }


    public class ConnectedServer {

        public int LatencyCache;

        public int CacheServerID;

    }

 

    public class Request {

        public int VideoID;

        public int EndPointID;

        public int NumberOfRequests;

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
        public List<Request> Requests = new List<Request>();
    }
}
