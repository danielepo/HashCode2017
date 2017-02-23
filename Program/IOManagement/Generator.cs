﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program.IOManagement
{
    public class Generator : IGenerator
    {
        public void Generate(string path, Dictionary<ConnectedServer, List<Video>> result)
        {
            System.IO.File.WriteAllText(path,Convert(result));
        }

        public string Convert(Dictionary<ConnectedServer, List<Video>> result)
        {

            var sb = new StringBuilder();
            sb.Append(result.Count.ToString());
            sb.Append("\n");
            sb.Append(string.Join("\n",result.Select(x => $"{x.Key.CacheServerID} {string.Join(" ",x.Value.Select(y => y.ID))}")));
            return sb.ToString();
        }
    }
}