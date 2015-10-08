using System.Collections.Generic;
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
}
