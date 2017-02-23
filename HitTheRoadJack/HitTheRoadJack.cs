using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program;
using Program.IOManagement;

namespace HitTheRoadJack
{
    class HitTheRoadJack
    {
        static void Main(string[] args)
        {
            foreach (var fn in new [] {"trending_today"})
        
            {
                Console.WriteLine($"starting {fn}");
                ILoader loader = new Loader($"C:\\{fn}.in");
                IGenerator generator = new Generator($"C:\\{fn}.out");
                new Solver(loader, generator).Run();
                Console.WriteLine($"finished {fn}");
            }
        }
    }
}
