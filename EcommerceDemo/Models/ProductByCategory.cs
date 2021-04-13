using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class ProductByCategory
    {
        public int product_id { get; set; }
        public int compare_id { get; set; }
        public String product_name { get; set; }
        public String product_image { get; set; }
        public String product_description { get; set; }
        public int product_sale { get; set; }
        public float product_price { get; set; }
        public float rating { get; set; }
        public List <int> newProd { get; set; }
        public List<int> hotProd { get; set; }
        public List <int> bestProd { get; set; }
    }
}
