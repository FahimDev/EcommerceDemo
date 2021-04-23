using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class ProductCatagories
    {
        public int id { get; set; }
        [Required]
        public String catagory_name { get; set; }
        public String catagory_img_path { get; set; }
        public String policy { get; set; }
        public float small { get; set; }
        public float medium { get; set; }
        public float large { get; set; }
        public String unit { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
