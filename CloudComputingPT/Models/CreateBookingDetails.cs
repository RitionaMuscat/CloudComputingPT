using CloudComputingPT.Data;
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
        public IQueryable<BookingDetails> book_details()
        {
            var bookingDetails = from a in _applicationDBContext.BookingDetails
                                 select a;

            _bookingDetails = bookingDetails;
            return _bookingDetails;
        }

        public IQueryable<BookingDetails> GetBookingDetails()
        {
            var getAvailableBookings = from b in _applicationDBContext.BookingDetails
                                       where b.isBookingConfirmed == true
                                       select b;

            _bookingDetails = getAvailableBookings;
            return _bookingDetails;
        }
    }
}
