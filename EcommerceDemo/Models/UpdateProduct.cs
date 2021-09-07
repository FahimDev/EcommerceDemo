using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EcommerceDemo.Models
{
    public class UpdateProduct
    {
        [Required]
        public int prod_id { get; set; }
        [Required]
        public int category_id { get; set; }
        [Required]
        public String product_name { get; set; }
        [Required]
        public int company_id { get; set; }
        [Required]
        public String h_product_image { get; set; }
        [Required]
        public String video_url { get; set; }
        [Required]
        public String product_description { get; set; }
        [Required]
        public String packing_type { get; set; }
        [Required]
        public String product_material { get; set; }
        [Required]
        public String product_brand { get; set; }
        [Required]
        public String product_color { get; set; }
        [Required]
        public int minimum_order { get; set; }
        [Required]
        public int product_sell { get; set; }
        [Required]
        public int product_price { get; set; }
        [Required]
        public String h_image_one { get; set; }
        [Required]
        public String h_image_two { get; set; }
        [Required]
        public String h_image_three { get; set; }
        [Required]
        public String h_image_four { get; set; }
    }
}
