using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class AdminRegistration
    {
        [Required]
        public String full_name { get; set; }
        [Required]
        public String contact { get; set; }
        [Required]
        public String email { get; set; }
        [Required]
        public String location { get; set; }
        [Required]
        public String username { get; set; }
        [Required]
        public String password { get; set; }
        [Required]
        public int role_id { get; set; }
    }
}
