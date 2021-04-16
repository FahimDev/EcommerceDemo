using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models
{
    public class Discounts
    {
        [Required]
        public int id { get; set; }
        [Required]
        public String discount_title { get; set; }
        [Required]
        public String discount_code { get; set; }
        [Required]
        public int discount_range { get; set; }
        [Required]
        public int discount_percentage { get; set; }
        [Required]
        public DateTime discount_expired { get; set; }
        [Required]
        public int status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
