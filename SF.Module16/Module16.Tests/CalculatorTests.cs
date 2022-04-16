using System;
using NUnit.Framework;

namespace Module16.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        static Calculator calculator;

        [SetUp]
        public static void SetUp()
        {
            calculator = new Calculator();
        }

        [Test]
        public void Additional_MustReturnCorrectValue()
        {
            Assert.AreEqual(22, calculator.Additional(10, 12));
            //Assert.That(calculatorTest.Additional(10, 12), Is.EqualTo(22));
        }

        [Test]
        public void Subtraction_MustReturnCorrectValue()
        {
            Assert.AreEqual(-2, calculator.Subtraction(14, 16));
        }

        [Test]
        public void Miltiplication_MustReturnCorrectValue()
        {
            Assert.AreEqual(56, calculator.Miltiplication(7, 8));
        }

        [Test]
        public void Division_MustReturnCorrectValue()
        {
            Assert.AreEqual(3, calculator.Division(18, 6));
        }

        [Test]
        public void Division_MustThrowException()
        {
            Assert.Throws<ArgumentException>(() => calculator.Division(1, 0));
        }
    }
}
