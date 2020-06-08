using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        //public void MethodUndertest_Scenario_ExpectedBehavior()
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            //Arrange: Inicializar objetos
            var user = new User { IsAdmin = true };
            var reservation = new Reservation();

            //Act: Método que vamos a probar
            var result = reservation.CanBeCancelledBy(user);

            //Assert
            Assert.IsTrue(result);

        }

        [Test]
        public void CanBeCancelledBy_SameUserCancellingTheReservation_ReturnsTrue()
        {
            //Arrange
            var user = new User { IsAdmin = false };
            var reservation = new Reservation { MadeBy = user};
            //Act
            var result = reservation.CanBeCancelledBy(user);
            //Assert
            Assert.IsTrue(result);
            Assert.That(result, Is.True);
            Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_UserCancellingIsNeitherAdminNorTheCreator_ReturnsFalse()
        {
            //Arrange
            var reservation = new Reservation { MadeBy = new User()};
            var anotherUser = new User { IsAdmin = false };
            //Act
            var result = reservation.CanBeCancelledBy(anotherUser);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
