using System.Collections.Generic;
using System.Linq;

namespace Milliman.FileCalc
{
    public class Configuration
    {
        public Configuration()
        {
            Calculations = new List<Calculation>();
        }
        public IList<Calculation> Calculations { get; set; }
    }

    public class Calculation
    {
        public string Variable { get; set; }
        public ICalculation StatCalculation { get; set; }
        public ICalculation PeriodChoice { get; set; }
        public IList<decimal> Accumulated { get; }

        public Calculation()
        {
            Accumulated = new List<decimal>();
        } 
    }

    public interface ICalculation
    {
        string Key { get; }
        decimal Calc(IEnumerable<decimal> values);
    }

    public class Min : ICalculation
    {
        public string Key => "MinValue";

        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().Min();
        }
    }

    public class Max : ICalculation
    {
        public string Key => "MaxValue";

        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().Max();
        }
    }

    public class Average : ICalculation
    {
        public string Key => "Average";

        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().Average();
        }
    }

    public class Last : ICalculation
    {
        public string Key => "LastValue";

        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().Last();
        }
    }

    public class First : ICalculation
    {
        public string Key => "FirstValue";

        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().First();
        }
    }
}
