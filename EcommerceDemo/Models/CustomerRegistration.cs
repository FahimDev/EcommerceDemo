using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class CustomerRegistration
    {  
        [Required]
        [Display(Name = "Full Name")]
        public String full_name { get; set; }
        [Required]        
        public String contact { get; set; }
        [Required]
        public String email { get; set; }
        [Required]
        public String username { get; set; }
        [Required]
        public String password { get; set; }
        [Required]
        [Display(Name = "Socail Media Link")]
        public String social_media_link { get; set; }
        [Required]
        public String location { get; set; }
        [Required]
        public String city { get; set; }
        [Required]
        public int zip { get; set; }

    }
}
