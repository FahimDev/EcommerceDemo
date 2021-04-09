using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class ProductJoinCatagory
    {
        public int product_id { get; set; }
        public String catagory_name { get; set; }
        public String product_name { get; set; }
        public String product_details { get; set; }
        public String catagory_policy { get; set; }
        public String product_image { get; set; }
        public String video_url { get; set; }
        public String packing_type { get; set; }
        public String product_material { get; set; }
        public String product_brand { get; set; }
        public String product_price { get; set; }
        public String produc_sell { get; set; }
        public String minimum_order { get; set; }

        //---------------------------------------
        public float small { get; set; }
        public float medium { get; set; }
        public float large { get; set; }
        public String unit { get; set; }

        //----------------------------------------
        public String image1_path { get; set; }
        public String image2_path { get; set; }
        public String image3_path { get; set; }
        public String image4_path { get; set; }

    }
}
