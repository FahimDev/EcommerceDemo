using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class ViewProductsByCat
    {
        public int prodId { get; set; }
        public String prodName { get; set; }
        public String prodImage { get; set; }
        public float rating { get; set; }
        public int price { get; set; }

        public bool best { get; set; }
        public bool hot { get; set; }
        public bool newProd { get; set; }
        public int sale { get; set; }
    }
}
