namespace Milliman.FileCalc.Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CalculationRunner
    {
        private static char delimiter = '\t';

        public void Run(string configPath, string inputPath, string outputPath)
        {
            var config = this.LoadCalculations(configPath);

            var input = this.StreamInput(inputPath);

            var output = Calculator.RunCalculations(input, config);
            var formattedOutput = output.Select(o => String.Format("{0}{1}{2}", o.VarName, delimiter, o.Result));

            File.WriteAllLines(outputPath, formattedOutput);
        }
    
        private IEnumerable<Scene> StreamInput(string path)
        {
            var map = new SceneProjector(delimiter);
            return map.Map(File.ReadLines(path));
        }

        private IEnumerable<Calculation> LoadCalculations(string path)
        {
            var map = new CalculationProjector(delimiter);
            var input = File.ReadLines(path);
            return map.Map(input).ToList();
        }
    }
}