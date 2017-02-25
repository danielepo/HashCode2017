using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class LoadFile
    {
        public Data LoadFrom(string path)
        {
            Data data = new Data();

            System.IO.StreamReader file = new System.IO.StreamReader(path);

            string line1 = file.ReadLine();// L1
            string[] li = line1.Split(' ');
            data.NumberOfVideos = int.Parse(li[0]);
            data.NumberOfEndpoints = int.Parse(li[1]);
            data.NumberOfRequests = int.Parse(li[2]);
            data.NumberOfCacheServers = int.Parse(li[3]);
            data.CapacityOfCacheServer = int.Parse(li[4]);

            string line2 = file.ReadLine();// L2
            string[] vids = line2.Split(' ');
            for (int i = 0; i < vids.Length; i++)
            {
                Video v = new Video { ID = i, Size = int.Parse(vids[i]) };
                data.Videos.Add(v);
            }

            for (int i = 0; i < data.NumberOfEndpoints; i++)
            {
                string lineEp = file.ReadLine();// EndPoint
                string[] endP = lineEp.Split(' ');

                EndPoint ep = new EndPoint();
                ep.ID = i;
                ep.LatencyDataCenter = int.Parse(endP[0]);

                int connectedCaches = int.Parse(endP[1]);

                for (int j = 0; j < connectedCaches; j++)
                {
                    string lineConnC = file.ReadLine();// ConnecteCache
                    string[] cc = lineConnC.Split(' ');

                    ConnectedServer conn = new ConnectedServer();
                    conn.CacheServerID = int.Parse(cc[0]);
                    conn.LatencyCache = int.Parse(cc[1]);

                    ep.ConnectedServers.Add(conn);
                }

                data.Endpoint.Add(ep);
            }

            for (int k = 0; k < data.NumberOfRequests; k++)
            {
                string req = file.ReadLine();// ConnecteCache
                string[] crrc = req.Split(' ');

                Request r = new Request();
                r.VideoID = int.Parse(crrc[0]);
                r.EndPointID = int.Parse(crrc[1]);
                r.NumberOfRequests = int.Parse(crrc[2]);

                data.Requests.Add(r);
            }

            file.Close();

            return data;
        }
    }
}