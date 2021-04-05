using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models
{
    public class Products
    {
        public int id { get; set; }
        [Required]
        public int catagory_id { get; set; }
        //[Display(Name ="Product Name")]
        public String product_name { get; set; }
        public int company_id { get; set; }
        public String product_img { get; set; }
        public String video_url { get; set; }
        public String product_description { get; set; }
        public String packing_type { get; set; }
        public String product_material { get; set; }
        public String product_brand { get; set; }
        public String product_color { get; set; }
        public String minimum_order { get; set; }
        public String product_sell { get; set; }
        public String product_price { get; set; }

    }
}
