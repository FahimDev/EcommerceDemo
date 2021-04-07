using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class ProductImages
    {
        public int id { get; set; }
        [Required]
        public int product_id { get; set; }
        public String image1_path { get; set; }
        public String image2_path { get; set; }
        public String image3_path { get; set; }
        public String image4_path { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
