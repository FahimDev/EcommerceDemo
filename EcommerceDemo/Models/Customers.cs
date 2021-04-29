using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class Customers
    {
        public int id { get; set; }
        [Required]
        public int login_id { get; set; }
        public String full_name { get; set; }
        public String contact { get; set; }
        public String email { get; set; }
        public String social_media_link { get; set; }
        public String area { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String country { get; set; }
        public int zip { get; set; }
        public int company_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
