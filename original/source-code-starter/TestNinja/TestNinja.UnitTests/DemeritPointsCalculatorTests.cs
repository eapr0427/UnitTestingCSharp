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
    public class DemeritPointsCalculatorTests
    {

        //Podemos crear un solo método parametrizado para las velocidades negativas y que suepran la cota superior de velocidad (Siguientes 2 métodos)


        [Test] 
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int speed)
        {
            var demeritCalculator = new DemeritPointsCalculator();

            Assert.That(() => demeritCalculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }



        //[Test]
        //public void CalculateDemeritPoints_SpeedIsNegative_ThrowArgumentOutOfRangeException()
        //{
        //    var demeritCalculator = new DemeritPointsCalculator();

        //    Assert.That(() => demeritCalculator.CalculateDemeritPoints(-1),Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        //}

        //[Test]
        //public void CalculateDemeritPoints_SpeedIsGreatherThanMaxSpeed_ThrowArgumentOutOfRangeException()
        //{
        //    var demeritCalculator = new DemeritPointsCalculator();
        //    Assert.That(() => demeritCalculator.CalculateDemeritPoints(301), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        //}





        //Los cuatro métodos de prueba debajo siguen ún mismo patrón, es decir están probando que se retorne cero. Por lo que sería mejor combinarlos en una sola prueba con varios test cases.
        // Por lo tanto creamos un método genérico con Parameterized Tests

        // De hecho se pueden agregar otros test case  al mismo método y que sea más genérico por ejemplo para cuando exceda en 5 kilometros o mas y deba retornar 1

        //[Test]
        //public void CalculateDemeritPoints_SpeedIsZero_ReturnZero()
        //{

        //}

        //[Test]
        //public void CalculateDemeritPoints_SpeedIsLowerThanSpeedLimit_ReturnZero()
        //{

        //}


        //[Test]
        //public void CalculateDemeritPoints_SpeedIsExactlyTheSpeedLimit_ReturnZero()
        //{

        //}

        //[Test]
        //public void CalculateDemeritPoints_SpeedExceedsSpeedLimitWithLessThanFiveKilomerters_ReturnZero()
        //{

        //}


        //[Test]
        //public void CalculateDemeritPoints_SpeedExceedsFiveKilomertersSpeedLimit_ReturnOne()
        //{

        //}

        [Test]
        [TestCase(0,0)]
        [TestCase(60,0)]
        [TestCase(65, 0)]
        [TestCase(68, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectedResult)
        {
            var calculator = new DemeritPointsCalculator();

            var points = calculator.CalculateDemeritPoints(speed);

            Assert.That(points, Is.EqualTo(expectedResult));
        }



        
    }
}
