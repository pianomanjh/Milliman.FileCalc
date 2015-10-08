using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Milliman.FileCalc
{
    public class Calculator
    {
        private readonly Configuration _config;

        public Calculator(Configuration config)
        {
            _config = config;
        }

        public IEnumerable<CalcResult> PerformAllCalculations(IEnumerable<Scene> input)
        {
            return _config.Calculations.Select(x => Crunch(input, x));
        }

        public CalcResult Crunch(IEnumerable<Scene> input, Calculation c)
        {
            return new CalcResult()
            {
                VarName = c.Variable,
                Result = c.StatCalculation.Calc(input.Where(x => x.VarName == c.Variable).Select(x => c.PeriodChoice.Calc(x.Values)))
            };
        } 

        public decimal FindPeriod(IEnumerable<decimal> input, ICalculation periodChoice)
        {
            return periodChoice.Calc(input);
        }
    }

    public struct Scene
    {
        public Scene(string varName, IEnumerable<decimal> values)
        {
            VarName = varName;
            Values = values;
        }

        public string VarName;
        public IEnumerable<decimal> Values;
    }

    public interface IProjector<T>
    {
        IEnumerable<T> Map(IEnumerable<string> input);
    }

    public class SceneProjector : IProjector<Scene>
    {
        private readonly char _delimiter;

        public SceneProjector(char delimiter)
        {
            _delimiter = delimiter;
        }

        public IEnumerable<Scene> Map(IEnumerable<string> input)
        {
            foreach (var i in input)
            {
                var values = i.Split(_delimiter);
                yield return new Scene(values[1], values.Skip(2).Select(decimal.Parse));
            }
        }
    }

    public class FileParser
    {
        public IEnumerable<string> Do(string path, bool skipFirst = true)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                if (skipFirst)
                    sr.ReadLine();

                yield return sr.ReadLine();
            }
        }
    }
}
