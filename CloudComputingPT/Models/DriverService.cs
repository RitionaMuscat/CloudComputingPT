using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingPT.Models
{
    public class DriverService
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("AspNetUsers")]
        public Guid driverId { get; set; }
        public bool luxury { get; set; }
        public bool economy { get; set; }
        public bool business { get; set; }
        public int capacity { get; set; }
        public string condition { get; set; }
        public string registrationPlate { get; set; }
        public bool airCondition { get; set; }
        public bool foodOrDrinks { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Picture { get; set; }


    }
}
