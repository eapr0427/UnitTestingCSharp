using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository;


        [SetUp]
        public void SetUp()
        {
            //Programamos mock
            _existingBooking = new Booking()
            {
                Id = 2,
                ArrivalDate = ArriveOn(1979, 4, 8),
                DepartureDate = DepartOn(1979, 4, 11),
                Reference = "FJPS"
            };

            //ARRANGE
            //Creamos mock
            _repository = new Mock<IBookingRepository>();


            _repository.Setup(br => br.GetActiveBookings(1))
                                .Returns(new List<Booking>
                                {
                                    _existingBooking

                                }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            // ACT
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate =  Before(_existingBooking.ArrivalDate, days: 2), // ArriveOn(1979, 4, 1),
                DepartureDate = Before(_existingBooking.ArrivalDate), //DepartOn(1979, 4, 3),
            }, _repository.Object);

            //Assert
            Assert.That(result, Is.Empty);
        }


        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
        {
            // ACT
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate), // ArriveOn(1979, 4, 1),
                DepartureDate = After(_existingBooking.ArrivalDate), //DepartOn(1979, 4, 3),
            }, _repository.Object);

            //Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }


        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
        {
            // ACT
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate), // ArriveOn(1979, 4, 1),
                DepartureDate = After(_existingBooking.DepartureDate), //DepartOn(1979, 4, 3),
            }, _repository.Object);

            //Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMidleOfAnExistingBooking_ReturnExistingBookingsReference()
        {
            // ACT
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate), // ArriveOn(1979, 4, 1),
                DepartureDate = Before(_existingBooking.DepartureDate), //DepartOn(1979, 4, 3),
            }, _repository.Object);

            //Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsInTheMidleOfAnExistingBookingButFinishesAfter_ReturnExistingBookingsReference()
        {
            // ACT
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate), // ArriveOn(1979, 4, 1),
                DepartureDate = After(_existingBooking.DepartureDate), //DepartOn(1979, 4, 3),
            }, _repository.Object);

            //Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            // ACT
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate), // ArriveOn(1979, 4, 1),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2), //DepartOn(1979, 4, 3),
            }, _repository.Object);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            // ACT
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate), // ArriveOn(1979, 4, 1),
                DepartureDate = After(_existingBooking.DepartureDate), //DepartOn(1979, 4, 3),
                Status = "Cancelled"
            }, _repository.Object);

            //Assert
            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
