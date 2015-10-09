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
            var output = sut.Map(new[] { input });

            Assert.That(output.First().VarName, Is.EqualTo("AvePolLoanYield"));
            Assert.That(output.First().Values, Is.EqualTo(new decimal[] { 0.00m, 0.04m }));
        }

        [Test]
        public void MultipleLineLoadsScenes()
        {
            string input =
                "1\tAvePolLoanYield\t0.00\t0.04\n" +
                "1\tCashPrem\t3.23\t4.12";

            IProjector<Scene> sut = new SceneProjector('\t');
            var output = sut.Map(input.Split('\n')).ToList();

            Assert.That(output[0].VarName, Is.EqualTo("AvePolLoanYield"));
            Assert.That(output[0].Values, Is.EqualTo(new decimal[] { 0.00m, 0.04m }));
            Assert.That(output[1].VarName, Is.EqualTo("CashPrem"));
            Assert.That(output[1].Values, Is.EqualTo(new decimal[] { 3.23m, 4.12m }));
        }

        [Test]
        public void SingleLineLoadsAsConfiguration()
        {
            string input =
                "CashPrem\tAverage\tMaxValue";

            IProjector<Calculation> sut = new CalculationProjector('\t');
            var output = sut.Map(new[] { input });

            var calc = output.First();
            Assert.That(calc.Variable, Is.EqualTo("CashPrem"));
            Assert.That(calc.StatCalculation, Is.InstanceOf(typeof(Average)));
            Assert.That(calc.PeriodChoice, Is.InstanceOf(typeof(Max)));

        }
    }
}
