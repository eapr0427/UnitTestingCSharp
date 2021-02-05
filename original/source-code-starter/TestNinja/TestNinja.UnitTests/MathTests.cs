using NUnit.Framework;
using TestNinja.Fundamentals;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        //SetUp
        //TearDown : Often used with integration tests... in order to clean UP data after  each test
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            var result = _math.Add(19, 79);
            Assert.That(result, Is.EqualTo(98));
        }


        [Test]
        [TestCase(8,4,8)]
        [TestCase(19,79,79)]
        [TestCase(2,2,2)]
        //[Ignore("Because it’s too slow")]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a,b);
            //Assert.AreEqual(result,8);
            Assert.That(result, Is.EqualTo(expectedResult));
        }


        //[Test]
        //public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
        //{
        //    var result = _math.Max(8, 4);
        //    //Assert.AreEqual(result,8);
        //    Assert.That(result, Is.EqualTo(8));
        //}

        //[Test]
        //public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
        //{
        //    var result = _math.Max(19, 79);
        // //   Assert.AreEqual(result, 8);
        //    Assert.That(result, Is.EqualTo(79));
        //}

        //[Test]
        //public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        //{
        //    var result = _math.Max(2, 2);
        //  //  Assert.AreEqual(result, 2);
        //    Assert.That(result, Is.EqualTo(2));
        //}

        [Test]
        public void GetOddNumbers_LimitIsGreatherThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(8);

            //Too general
            //Assert.That(result, Is.Not.Empty);

            //Assert.That(result.Count(), Is.EqualTo(4));

            // More specific

            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5, 7 }));

           // Assert.That(result, Is.Ordered);
            // Assert.That(result, Is.Unique);

        }
    }
}
