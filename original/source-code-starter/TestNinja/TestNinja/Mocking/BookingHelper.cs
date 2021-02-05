using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {

        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;


            //var unitOfWork = new UnitOfWork();
            //Refactorizamos esta dependencia externa en una clase a parte
            //var bookings =
            //    unitOfWork.Query<Booking>()
            //        .Where(
            //            b => b.Id != booking.Id && b.Status != "Cancelled");

            var bookings = bookingRepository.GetActiveBookings(booking.Id);

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate &&
                        b.ArrivalDate < booking.DepartureDate
                        //booking.ArrivalDate >= b.ArrivalDate
                        //&& booking.ArrivalDate < b.DepartureDate
                        //|| booking.DepartureDate > b.ArrivalDate
                        //&& booking.DepartureDate <= b.DepartureDate  // ESTE CODIGO TIENE BUGS DETECTADOS POR EL UNIT TESTING
                        );

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}