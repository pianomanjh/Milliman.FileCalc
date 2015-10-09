using System;
using System.Text;
using System.Threading.Tasks;

namespace Milliman.FileCalc.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 3)
                Console.WriteLine(
                    "Specify configuration file as first argument, data file as second, and output as third");

            new CalculationRunner().Run(args[0], args[1], args[2]);
        }
    }
}
