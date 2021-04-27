using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class ShowAllCategories
    {
        public int id { get; set; }
        public String category_name { get; set; }
        public String category_img { get; set; }
        public int product_count { get; set; }
    }
}
