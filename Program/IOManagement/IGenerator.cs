using System.Collections.Generic;

namespace Program.IOManagement
{
    public interface IGenerator
    {
        void Generate(string path, Dictionary<ConnectedServer, List<Video>> result);
    }
}