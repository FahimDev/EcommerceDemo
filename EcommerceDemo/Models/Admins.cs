using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class Admins
    {
        public int id { get; set; }
        [Required]
        public int login_id { get; set; }
        public String full_name { get; set; }
        public String contact { get; set; }
        public String email { get; set; }
        public String location { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
