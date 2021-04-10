using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class Reviews
    {
        public int id { get; set; }
        [Required]
        public int customer_id { get; set; }
        public int product_id { get; set; }
        public String review_title { get; set; }
        public String review_body { get; set; }
        public float rating { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
