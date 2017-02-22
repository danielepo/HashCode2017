using InputFileLoader;
using OutputFileGenerator;

namespace Program
{
    public class Program
    {
        private readonly ILoader _loader;
        private readonly IGenerator _generator;

        public Program(ILoader loader, IGenerator generator)
        {
            _loader = loader;
            _generator = generator;
        }

        public void Run()
        {
            var input = _loader.Load();
            //fai calcoli
            _generator.Generate();
        }
    }
}
