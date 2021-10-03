using System;
using System.ComponentModel.DataAnnotations;

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
