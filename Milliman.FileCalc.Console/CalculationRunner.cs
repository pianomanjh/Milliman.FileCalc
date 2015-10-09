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
            var calculations = this.StreamInput(configPath, new CalculationProjector(delimiter)).ToList();

            var inputStream = this.StreamInput(inputPath, new SceneProjector(delimiter), true);

            var outputStream = Calculator.RunCalculations(inputStream, calculations);
            var formattedOutput = outputStream.Select(o => String.Format("{0}{1}{2}", o.VarName, delimiter, o.Result));

            File.WriteAllLines(outputPath, formattedOutput);
        }
    
        private IEnumerable<T> StreamInput<T>(string path, IProjector<T> map, bool skipHeader = false)
        {
            var input = File.ReadLines(path);
            if (skipHeader) input = input.Skip(1);
            return map.Map(input);
        }
    }
}