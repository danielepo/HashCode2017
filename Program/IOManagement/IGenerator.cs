using System.Collections.Generic;

namespace Program.IOManagement
{
    public interface IGenerator
    {
        void Generate(Dictionary<CacheServer, List<Video>> toDictionary);
    }
}