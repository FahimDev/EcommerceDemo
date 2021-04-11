using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class AdminRegistration
    {
        public String full_name { get; set; }
        public String contact { get; set; }
        public String email { get; set; }
        public String location { get; set; }
        public String username { get; set; }
        public String password { get; set; }
    }
}
