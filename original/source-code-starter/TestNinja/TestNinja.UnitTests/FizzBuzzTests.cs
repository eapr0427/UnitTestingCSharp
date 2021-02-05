using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_NumberIsDivisibleBy3and5_ReturnFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(30);
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        [Test]
        public void GetOutput_NumberIsDivisibleBy3Only_ReturnFizz()
        {
            var result = FizzBuzz.GetOutput(9);
            Assert.That(result, Is.EqualTo("Fizz"));
        }
        [Test]
        public void GetOutput_NumberIsDivisibleBy5Only_ReturnBuzz()
        {
            var result = FizzBuzz.GetOutput(20);
            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_NumberIsNotDivisibleNeitherBy3Nor5_ReturnSameNumber()
        {
            var result = FizzBuzz.GetOutput(11);
            Assert.That(result, Is.EqualTo("11"));
        }
    }
}
