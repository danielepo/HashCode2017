using System.Collections.Generic;

namespace Program.IOManagement
{
    public class Generator : IGenerator
    {
        public void Generate()
        {

        }

        public string Convert(Dictionary<CacheServer, List<Video>> result)
        {
            return result.Count.ToString();
        }
    }
}