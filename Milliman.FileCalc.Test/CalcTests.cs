using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Diagnostics;

namespace Milliman.FileCalc.Test
{
    [TestFixture]
    public class CalcTests
    {
        [Test]
        public void NoInputZeroResult()
        {
            var calculations = new List<Calculation>
                                   {
                                       new Calculation()
                                           {
                                               Variable = "CashPrem",
                                               StatCalculation = new Average(),
                                               PeriodChoice = new Max()
                                           }
                                   };
            
            var result = Calculator.RunCalculations(new List<Scene> {}, calculations).FirstOrDefault();

            Assert.AreEqual("CashPrem", result.VarName);
            Assert.AreEqual(0, result.Result);
        }

        [Test]
        public void SingleLineReturnsResultOfStatCalc()
        {
            // arrange
            var calculations = new List<Calculation>
                                   {
                                       new Calculation()
                                           {
                                               Variable = "CashPrem",
                                               StatCalculation = new Average(),
                                               PeriodChoice = new Max()
                                           }
                                   };
            var samples = new List<Scene>()
            {
                new Scene("CashPrem", new[] { 1m,1m,3m,3m})
            };

            //act 
            var result = Calculator.RunCalculations(samples, calculations).FirstOrDefault();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CashPrem", result.VarName);
            Assert.AreEqual(3, result.Result);
        }

        [Test]
        public void TwoLinesReturnsResultOfBothCalc()
        {
            // arrange
            var calculations = new[]
                                   {
                                       new Calculation()
                                           {
                                               Variable = "CashPrem",
                                               StatCalculation = new Average(),
                                               PeriodChoice = new Max()
                                           }
                                   };
            var lines = new List<Scene>();
            lines.Add(new Scene("CashPrem", new[] { 1m,1m,3m,3m }));
            lines.Add(new Scene("CashPrem", new[] { 0m,0m,0m,1m }));

            //act 
            var result = Calculator.RunCalculations(lines, calculations).FirstOrDefault();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CashPrem", result.VarName);
            Assert.AreEqual(2, result.Result);
        }

        [Test]
        public void ThreeLinesFiltersVariable()
        {
            // arrange
            var calculations = new[] { new Calculation() { Variable = "CashPrem", StatCalculation = new Average(), PeriodChoice = new Max() }};

            var lines = new List<Scene>();
            lines.Add(new Scene("CashPrem", new[] { 1m, 1m, 3m, 3m }));
            lines.Add(new Scene("NoThanks", new[] { 50m, 12m, 21m, 31m }));
            lines.Add(new Scene("CashPrem", new[] { 0m, 0m, 0m, 1m }));

            //act 
            var result = Calculator.RunCalculations(lines, calculations).FirstOrDefault();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CashPrem", result.VarName);
            Assert.AreEqual(2, result.Result);
        }

        [Test]
        public void MultipleCalculationsYieldsMultipleResults()
        {
            // arrange
            var calculations = new[] {
                new Calculation() { Variable = "CashPrem", StatCalculation = new Average(), PeriodChoice = new Max() },
                new Calculation() { Variable = "NoThanks", StatCalculation = new Max(), PeriodChoice = new Last() }};

            var lines = new List<Scene>();
            lines.Add(new Scene("CashPrem", new[] { 1m, 1m, 3m, 3m }));
            lines.Add(new Scene("NoThanks", new[] { 50m, 12m, 21m, 30m }));
            lines.Add(new Scene("NoThanks", new[] { 50m, 43m, 3m, 10m }));
            lines.Add(new Scene("CashPrem", new[] { 0m, 0m, 0m, 1m }));

            //act 
            var result = Calculator.RunCalculations(lines, calculations).ToList().Skip(1).First();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("NoThanks", result.VarName);
            Assert.AreEqual(30, result.Result);
        }
    }
}
