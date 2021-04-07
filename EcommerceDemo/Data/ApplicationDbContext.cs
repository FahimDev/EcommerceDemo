using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EcommerceDemo.Models;

namespace EcommerceDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Login { get; set; }


        public DbSet<Products> Products { get; set; }


        public DbSet<ProductCatagories> ProductCatagories { get; set; }


        public DbSet<ProductImages> ProductImages { get; set; }


        public DbSet<ProductVolumes> ProductVolumes { get; set; }
    }
}
