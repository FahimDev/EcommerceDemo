using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models
{
    public class Logins
    {
        public int id { get; set; }
        [Required]
        public int role_id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String token { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
