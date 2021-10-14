using CloudComputingPT.Data;
using System;
using System.Linq;

namespace CloudComputingPT.Models
{
    public class CreateBookingDetails
    {
        ApplicationDbContext _applicationDBContext;
        public CreateBookingDetails(ApplicationDbContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public IQueryable<BookingDetails> _bookingDetails { get; set; }
        public IQueryable<BookingDetails> book_details(Guid UserId)
        {
            var bookingDetails = from a in _applicationDBContext.BookingDetails
                                 where a.passengerId.Equals(UserId)
                                 select a;

            _bookingDetails = bookingDetails;
            return _bookingDetails;
        }

        public IQueryable<BookingDetails> GetConfirmedBookingDetails()
        {
            var getAvailableBookings = from b in _applicationDBContext.BookingDetails
                                       where b.isBookingConfirmed == true && b.DriverDetails == null
                                       select b;

            _bookingDetails = getAvailableBookings;
            return _bookingDetails;
        }

        public IQueryable<BookingDetails> GetBookingDetailsById(Guid id)
        {
            var getAvailableBookings = from b in _applicationDBContext.BookingDetails
                                       where b.isBookingConfirmed == true && b.Id.Equals(id)
                                       select b;

            _bookingDetails = getAvailableBookings;
            return _bookingDetails;
        }
    }
}
