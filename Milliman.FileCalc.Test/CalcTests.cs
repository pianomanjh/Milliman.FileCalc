using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Milliman.FileCalc.Test
{
    [TestFixture]
    public class CalcTests
    {
        private Calculator sut;

        [Test]
        public void NoInputZeroResult()
        {
            Configuration config = new Configuration();
            config.Calculations.Add(new Calculation() { Variable = "CashPrem", StatCalculation = new Average(), PeriodChoice = new Max() });

            sut = new Calculator(config);
            var result = sut.PerformAllCalculations(new List<Scene> {}).FirstOrDefault();

            Assert.AreEqual("CashPrem", result.VarName);
            Assert.AreEqual(0, result.Result);
        }

        [Test]
        public void SingleLineReturnsResultOfStatCalc()
        {
            // arrange
            Configuration config = new Configuration();
            config.Calculations.Add(new Calculation() { Variable = "CashPrem", StatCalculation = new Average(), PeriodChoice = new Max()});

            var samples = new List<Scene>()
            {
                new Scene { VarName = "CashPrem", Values = new[] { 1m,1m,3m,3m}}
            };

            //act 
            sut = new Calculator(config);
            var result = sut.PerformAllCalculations(samples).FirstOrDefault();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CashPrem", result.VarName);
            Assert.AreEqual(3, result.Result);
        }

        [Test]
        public void TwoLinesReturnsResultOfBothCalc()
        {
            // arrange
            Configuration config = new Configuration();
            config.Calculations.Add(new Calculation() { Variable = "CashPrem", StatCalculation = new Average(), PeriodChoice = new Max() });

            var lines = new List<Scene>();
            lines.Add(new Scene("CashPrem", new[] { 1m,1m,3m,3m }));
            lines.Add(new Scene("CashPrem", new[] { 0m,0m,0m,1m }));

            //act 
            sut = new Calculator(config);
            var result = sut.PerformAllCalculations(lines).FirstOrDefault();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CashPrem", result.VarName);
            Assert.AreEqual(2, result.Result);
        }

        [Test]
        public void ThreeLinesFiltersVariable()
        {
            // arrange
            Configuration config = new Configuration();
            config.Calculations.Add(new Calculation() { Variable = "CashPrem", StatCalculation = new Average(), PeriodChoice = new Max() });

            var lines = new List<Scene>();
            lines.Add(new Scene("CashPrem", new[] { 1m, 1m, 3m, 3m }));
            lines.Add(new Scene("NoThanks", new[] { 50m, 12m, 21m, 31m }));
            lines.Add(new Scene("CashPrem", new[] { 0m, 0m, 0m, 1m }));

            //act 
            sut = new Calculator(config);
            var result = sut.PerformAllCalculations(lines).FirstOrDefault();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CashPrem", result.VarName);
            Assert.AreEqual(2, result.Result);
        }

    }
}
