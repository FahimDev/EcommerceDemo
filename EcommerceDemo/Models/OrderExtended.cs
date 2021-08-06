using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class OrderExtended
    {
        public int id { get; set; }
        public int total_price { get; set; }
        public int total_quantity { get; set; }
        public DateTime created_at { get; set; }
        public string ordered_by { get; set; }
        public int customer_id { get; set; }
    }
}
