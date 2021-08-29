using System;
using FabricGroup.TestTask.ConsoleRunner.Ledger;
using NUnit.Framework;

namespace FabricGroup.TestTask.Ledger.Tests
{
    public class BalanceCalculatorTests
    {
        private BalanceCalculator _calc;

        [SetUp]
        public void Setup()
        {
            _calc = new BalanceCalculator();
        }

        [Test]
        public void TestSmallLoan()
        {
            var bd = new BorrowerData(1, 1, 0.2m, 0, 0);
            var r1 = _calc.Calculate(bd, 1);
            Assert.AreEqual(1, r1.AmountPaid);
            Assert.AreEqual(1, r1.EmisLeft);
            var r2 = _calc.Calculate(bd, 2);
            Assert.AreEqual(2, r2.AmountPaid);
            Assert.AreEqual(0, r2.EmisLeft);
        }
        [Test]
        public void TestFarEmi()
        {
            var bd = new BorrowerData(1, 1, 0.2m, 0, 0);
            var r1 = _calc.Calculate(bd, 12345);
            Assert.AreEqual(2, r1.AmountPaid);
            Assert.AreEqual(0, r1.EmisLeft);
        }
        [Test]
        public void TestLastEmi()
        {
            var bd = new BorrowerData(1, 1000, 0.2m, 99, 1);
            var r1 = _calc.Calculate(bd, 12);
            Assert.AreEqual(1200, r1.AmountPaid);
            Assert.AreEqual(0, r1.EmisLeft);
            var r2 = _calc.Calculate(bd, 11);
            Assert.AreEqual(1199, r2.AmountPaid);
            Assert.AreEqual(1, r2.EmisLeft);
        }
        [Test]
        public void TestLoanOnlyDale()
        {
            var bd = new BorrowerData(5, 10000, 0.04m, 0, 0);
            var r1 = _calc.Calculate(bd, 5);
            Assert.AreEqual(1000, r1.AmountPaid);
            Assert.AreEqual(55, r1.EmisLeft);
            var r2 = _calc.Calculate(bd, 40);
            Assert.AreEqual(8000, r2.AmountPaid);
            Assert.AreEqual(20, r2.EmisLeft);
        }

        [Test]
        public void TestLoanOnlyHarry()
        {
            var bd = new BorrowerData(2, 2000, 0.02m, 0, 0);
            var r1 = _calc.Calculate(bd, 12);
            Assert.AreEqual(1044, r1.AmountPaid);
            Assert.AreEqual(12, r1.EmisLeft);
            var r2 = _calc.Calculate(bd, 0);
            Assert.AreEqual(0, r2.AmountPaid);
            Assert.AreEqual(24, r2.EmisLeft);
        }

        [Test]
        public void TestWithLumpDale()
        {
            var bd = new BorrowerData(1, 5000, 0.06m, 1000, 5);
            var r1 = _calc.Calculate(bd, 3);
            Assert.AreEqual(1326, r1.AmountPaid);
            Assert.AreEqual(9, r1.EmisLeft);
            var r2 = _calc.Calculate(bd, 6);
            Assert.AreEqual(3652, r2.AmountPaid);
            Assert.AreEqual(4, r2.EmisLeft);
        }

        [Test]
        public void TestWithLumpHarry()
        {
            var bd = new BorrowerData(3, 10000, 0.07m, 5000, 10);
            var r1 = _calc.Calculate(bd, 12);
            Assert.AreEqual(9044, r1.AmountPaid);
            Assert.AreEqual(10, r1.EmisLeft);
        }

        [Test]
        public void TestWithLumpShelly()
        {
            var bd = new BorrowerData(2, 15000, 0.09m, 7000, 12);
            var r1 = _calc.Calculate(bd, 12);
            Assert.AreEqual(15856, r1.AmountPaid);
            Assert.AreEqual(3, r1.EmisLeft);
        }
    }
}