using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingPT.Models
{
    public class BookingDetails
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("AspNetUsers")]
        public Guid passengerId { get; set; }

        [ForeignKey("Categories")]
        public string residingAdress { get; set; }

        public string destinationAddress { get; set; }

        public bool isBookingConfirmed { get; set; }



    }
}
