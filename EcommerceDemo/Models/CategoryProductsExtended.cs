using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class CategoryProductsExtended
    {
        public int id { get; set; }
        public String catagory_name { get; set; }
        public String catagory_img_path { get; set; }
        public List<Products> products { get; set; }

    }
}
