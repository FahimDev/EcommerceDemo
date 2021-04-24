using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EcommerceDemo.Models
{
    public class AddNewProduct
    {
        [Required]
        public int category_id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public String product_name { get; set; }
        [Required]
        public int company_id { get; set; }
        [Required]
        [Display(Name = "Product Display Image")]
        public String h_product_image { get; set; }
        [Required]
        [Display(Name = "Product Video URL")]
        public String video_url { get; set; }
        [Required]
        [Display(Name = "Product Description")]
        public String product_description { get; set; }
        [Required]
        [Display(Name = "Packing Type")]
        public String packing_type { get; set; }
        [Required]
        [Display(Name = "Product Ingredient(s)")]
        public String product_material { get; set; }
        [Required]
        [Display(Name = "Product Brand")]
        public String product_brand { get; set; }
        [Required]
        [Display(Name = "Product Color")]
        public String product_color { get; set; }
        [Required]
        [Display(Name = "Minimum order level")]
        public String minimum_order { get; set; }
        [Required]
        [Display(Name = "Product Sale")]
        public String product_sell { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public String product_price { get; set; }
        [Required]
        [Display(Name = "Product Image [1]")]
        public String h_image_one { get; set; }
        [Required]
        [Display(Name = "Product Image [2]")]
        public String h_image_two { get; set; }
        [Required]
        [Display(Name = "Product Image [3]")]
        public String h_image_three { get; set; }
        [Required]
        [Display(Name = "Product Image [4]")]
        public String h_image_four { get; set; }

    }
}
