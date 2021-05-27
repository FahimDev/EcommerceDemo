using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models
{
    public class Ordered_Products
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int order_id { get; set; }
        [Required]
        public int product_id { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
