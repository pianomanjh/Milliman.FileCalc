using System.Linq;

namespace Milliman.FileCalc.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class CalcWithProjectorTests
    {
        [Test]
        public void RawInputAndConfigIsCalculated()
        {
            // parse streamed input
            string formattedInput =
                "1\tAvePolLoanYield\t0.00\t0.04\n" +
                "1\tCashPrem\t3.23\t4.12";

            var sceneProjector = new SceneProjector('\t');
            var inputStream = sceneProjector.Map(formattedInput.Split('\n'));

            // parse calculations
            string formattedConfig =
                "CashPrem\tAverage\tMaxValue\n" +
                "AvePolLoanYield\tMaxValue\tLastValue";

            var calcProjector = new CalculationProjector('\t');
            var config = calcProjector.Map(formattedConfig.Split('\n')).ToList();

            // run calc
            var output = Calculator.RunCalculations(inputStream, config).ToList();

            Assert.That(output[0].VarName, Is.EqualTo("CashPrem"));
            Assert.That(output[0].Result, Is.EqualTo(4.12m));
            Assert.That(output[1].VarName, Is.EqualTo("AvePolLoanYield"));
            Assert.That(output[1].Result, Is.EqualTo(0.04m));

        }
    }
}
