namespace Milliman.FileCalc
{
    using System.Collections.Generic;

    public class CalculationProjector : IProjector<Calculation>
    {
        private readonly char _delimiter;

        private static IDictionary<string, ICalculation> keyedCalculations = new Dictionary<string, ICalculation>()
                                                                                 {
                                                                                     { "Average", new Average() },
                                                                                     { "MaxValue", new Max() },
                                                                                     { "MinValue", new Min() },
                                                                                     { "FirstValue", new First() },
                                                                                     { "LastValue", new Last() }
                                                                                 };  

        public CalculationProjector(char delimiter)
        {
            this._delimiter = delimiter;
        }

        public IEnumerable<Calculation> Map(IEnumerable<string> input)
        {
            foreach (var i in input)
            {
                var values = i.Split(this._delimiter);
                yield return
                    new Calculation
                        {
                            Variable = values[0],
                            StatCalculation = keyedCalculations[values[1]],
                            PeriodChoice = keyedCalculations[values[2]]
                        };
            }
        }
    }
}