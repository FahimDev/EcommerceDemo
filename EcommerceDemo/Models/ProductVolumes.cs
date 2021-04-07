using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Models
{
    public class ProductVolumes
    {
        public int id { get; set; }
        [Required]
        public float small { get; set; }
        public float medium { get; set; }
        public float large { get; set; }
        public String unit { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
