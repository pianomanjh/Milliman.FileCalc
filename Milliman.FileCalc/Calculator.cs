using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Milliman.FileCalc
{
    public static class Calculator
    {
        public static IEnumerable<KeyedResult> RunCalculations(IEnumerable<Scene> input, IReadOnlyList<Calculation> calculations)
        {
            foreach (var scene in input)
            {
                foreach (var calc in calculations.Where(x => x.Variable == scene.VarName))
                {
                    calc.Accumulator.Add(calc.PeriodChoice.Calc(scene.Values));
                }
            }

            return
                calculations.Select(x => new KeyedResult { Result = x.StatCalculation.Calc(x.Accumulator), VarName = x.Variable });
        }
    }
}
