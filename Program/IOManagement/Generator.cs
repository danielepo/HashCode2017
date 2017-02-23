using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program.IOManagement
{
    public class Generator : IGenerator
    {
        private readonly string path;

        public Generator(string path)
        {
            this.path = path;
        }

        public void Generate(string content)
        {
            System.IO.File.WriteAllText(path,content);
        }

        

        public string Convert(Dictionary<int, List<Video>> result)
        {

            var sb = new StringBuilder();
            sb.Append(result.Count.ToString());
            sb.Append("\n");
            sb.Append(string.Join("\n",result.Select(x => $"{x.Key} {string.Join(" ",x.Value.Select(y => y.ID))}")));
            return sb.ToString();
        }
    }
}