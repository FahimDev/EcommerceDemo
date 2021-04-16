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

        //public DbSet<Login> Login { get; set; }


        public DbSet<Products> Products { get; set; }


        public DbSet<ProductCatagories> ProductCatagories { get; set; }


        public DbSet<ProductImages> ProductImages { get; set; }


        public DbSet<ProductVolumes> ProductVolumes { get; set; }


        public DbSet<Admins> Admins { get; set; }


        public DbSet<Roles> EmpRoles { get; set; }


        public DbSet<Logins> Logins { get; set; }


        public DbSet<Reviews> Reviews { get; set; }


        public DbSet<Orders> Orders { get; set; }


        public DbSet<Ordered_Products> Ordered_Products { get; set; }


        public DbSet<Discounts> Discounts { get; set; }
    }
}
