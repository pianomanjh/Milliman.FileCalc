using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Milliman.FileCalc
{
    public static class Calculator
    {
        public static IEnumerable<KeyedResult> RunCalculations(IEnumerable<Scene> input, IEnumerable<Calculation> c)
        {
            foreach (var scene in input)
            {
                foreach (var calc in c.Where(x => x.Variable == scene.VarName))
                     calc.Accumulated.Add(calc.PeriodChoice.Calc(scene.Values));
            }

            return
                c.Select(x => new KeyedResult { Result = x.StatCalculation.Calc(x.Accumulated), VarName = x.Variable });
        }
    }
}
