using Program.IOManagement;

namespace Program
{
    public class Solver
    {
        private readonly ILoader _loader;
        private readonly IGenerator _generator;

        public Solver(ILoader loader, IGenerator generator)
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
