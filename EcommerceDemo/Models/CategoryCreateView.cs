using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EcommerceDemo.Models
{
    public class CategoryCreateView
    {
        public int id { get; set; }
        [Required]
        public String category_name { get; set; }
        [Required]
        public String category_unit { get; set; }
        [Required]
        public float volume_small { get; set; }
        [Required]
        public float volume_medium { get; set; }
        [Required]
        public float volume_large { get; set; }
        [Required]
        public String category_policy { get; set; }
        [Required]
        public IFormFile category_image { get; set; }
        public string category_img_path { get; set; }
        public String imageblob { get; set; }

        public string category_banner_image { get; set; }
        public String imageblob_banner { get; set; }

    }
}
