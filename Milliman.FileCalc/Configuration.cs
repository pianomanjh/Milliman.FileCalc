using System.Collections.Generic;
using System.Linq;

namespace Milliman.FileCalc
{
    public class Calculation
    {
        public string Variable { get; set; }
        public ICalculation StatCalculation { get; set; }
        public ICalculation PeriodChoice { get; set; }
        public IList<decimal> Accumulator { get; }

        public Calculation()
        {
            this.Accumulator = new List<decimal>();
        } 
    }

    public interface ICalculation
    {
        decimal Calc(IEnumerable<decimal> values);
    }

    public class Min : ICalculation
    {
        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().Min();
        }
    }

    public class Max : ICalculation
    {
        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().Max();
        }
    }

    public class Average : ICalculation
    {
        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().Average();
        }
    }

    public class Last : ICalculation
    {
        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().Last();
        }
    }

    public class First : ICalculation
    {
        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.DefaultIfEmpty().First();
        }
    }
}
