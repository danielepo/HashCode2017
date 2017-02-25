using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program;
using Program.IOManagement;

namespace HitTheRoadJack
{
    internal class HitTheRoadJack
    {
        private static void Main(string[] args)
        {
            var inputs = new[] {
                "me_at_the_zoo",
                "videos_worth_spreading",
                "trending_today",
                "kittens"
            };
            foreach (var fn in inputs)

            {
                Console.WriteLine($"starting {fn}");
                ILoader loader = new Loader($"C:\\{fn}.in");
                IGenerator generator = new Generator($"{fn}.out");
                new RandomSolver(loader, generator).Run();
                Console.WriteLine($"finished {fn}");
            }
        }
    }
}