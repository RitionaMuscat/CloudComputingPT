using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudComputingPT.Models
{
    public class BookingDetails
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("AspNetUsers")]
        public Guid passengerId { get; set; }
        public string residingAdress { get; set; }
        public string destinationAddress { get; set; }
        public bool isBookingConfirmed { get; set; }
        public bool luxury { get; set; }
        public bool economy { get; set; }
        public bool business { get; set; }
    }
}
