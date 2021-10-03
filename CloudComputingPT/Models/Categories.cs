using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingPT.Models
{
    public class Categories
    {
        [Key]
        public Guid Id { get; set; }

        public string categories { get; set; }

        public double flatPrice { get; set; }
    }
}
