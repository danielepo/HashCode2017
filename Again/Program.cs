using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program;
using Program.IOManagement;

namespace Again
{
    class HitTheRoadJack
    {
        static void Main(string[] args)
        {
            foreach (var fn in new [] {"kittens"})
        
            {
                Console.WriteLine(string.Format("starting {0}", fn));
                ILoader loader = new Loader(string.Format("C:\\{0}.in", fn));
                IGenerator generator = new Generator(string.Format("C:\\{0}.out", fn));
                new Solver(loader, generator).Run();
                Console.WriteLine(string.Format("finished {0}", fn));
            }
        }
    }
}
