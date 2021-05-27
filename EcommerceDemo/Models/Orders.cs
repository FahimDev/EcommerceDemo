using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models
{
    public class Orders
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int customer_id { get; set; }
        public int discount_id { get; set; }
        [Required]
        public int status { get; set; }
        [Required]
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
