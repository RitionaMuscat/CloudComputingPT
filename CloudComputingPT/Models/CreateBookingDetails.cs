using CloudComputingPT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var bookingDetails = from a in _applicationDBContext.bookingDetails
                                 select a;

            _bookingDetails = bookingDetails;
            return _bookingDetails;

        }

        public IQueryable<BookingDetails> GetBookingDetails()
        {
            var getAvailableBookings = from b in _applicationDBContext.bookingDetails
                                       where b.isBookingConfirmed == true
                                       select b;

            _bookingDetails = getAvailableBookings;
            return _bookingDetails;
                                      
        }
    }
}
