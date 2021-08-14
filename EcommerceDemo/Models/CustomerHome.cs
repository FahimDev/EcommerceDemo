using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models
{
    public class CustomerHome
    {
        public Customers custormerInfo { get; set; }
        public List<OrderExtended> orders { get; set; }
    }
}
