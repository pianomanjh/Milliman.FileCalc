using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliman.FileCalc
{
    public class Calculator
    {
    }

    public interface IOperation
    {
        decimal Calc(IEnumerable<decimal> values);
    }

    public class Min : IOperation
    {
        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.Min();
        }
    }

    public class Max : IOperation
    {
        public decimal Calc(IEnumerable<decimal> values)
        {
            return values.Max();
        }
    }
}
