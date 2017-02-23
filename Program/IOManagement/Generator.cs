using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program.IOManagement
{
    public class Generator : IGenerator
    {
        public void Generate()
        {

        }

        public string Convert(Dictionary<CacheServer, List<Video>> result)
        {

            var sb = new StringBuilder();
            sb.Append(result.Count.ToString());
            sb.Append("\n");
            sb.Append(string.Join("\n",result.Select(x => $"{x.Key.ID} {string.Join(" ",x.Value.Select(y => y.ID))}")));
            return sb.ToString();
        }
    }
}