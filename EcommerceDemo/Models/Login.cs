using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models
{
    public class Login
    {
               
        public int id { get; set; }
        [Required]
        //[Display(Name ="Logins")]
        public int role_id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String token { get; set; }
    }
}
