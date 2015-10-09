using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliman.FileCalc.Test
{
    [TestFixture]
    public class ProjectorTests
    {
        [Test]
        public void SingleLineLoadsAsScene()
        {
            string input =
                "1\tAvePolLoanYield\t0.00\t0.04";

            IProjector<Scene> sut = new SceneProjector('\t');
            var output = sut.Map(input.Split('\n'));

            Assert.That(output.First().VarName, Is.EqualTo("AvePolLoanYield"));
            Assert.That(output.First().Values, Is.EqualTo(new decimal[] { 0.00m, 0.04m }));
        }

        [Test]
        public void SingleLineLoadsAsConfiguration()
        {
            string input =
                "CashPrem\tAverage\tMaxValue";

            IProjector<Calculation> sut = new CalculationProjector('\t');
            var output = sut.Map(new string[] { input });

            var calc = output.First();
            Assert.That(calc.Variable, Is.EqualTo("CashPrem"));
            Assert.That(calc.StatCalculation, Is.InstanceOf(typeof(Average)));
            Assert.That(calc.PeriodChoice, Is.InstanceOf(typeof(Max)));

        }
    }
}
