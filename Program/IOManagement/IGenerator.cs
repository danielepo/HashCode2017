using System.Collections.Generic;

namespace Program.IOManagement
{
    public interface IGenerator
    {
        string Convert(Dictionary<ConnectedServer, List<Video>> toDictionary);
    }
}