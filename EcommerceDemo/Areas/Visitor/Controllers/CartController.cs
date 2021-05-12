using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceDemo.Models;
using EcommerceDemo.Data;

namespace EcommerceDemo.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    public class CartController : Controller
    {
        private ApplicationDbContext _db;
        //private Context _context;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public dynamic GetProductDetails(string id)
        {
            System.Diagnostics.Debug.WriteLine("................" + id);
            string somevalue = id;
            var products = _db.Products.Where(p => somevalue.Contains(p.id.ToString())).ToList();
            return products;
        }
    }
}
