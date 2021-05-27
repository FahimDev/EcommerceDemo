using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EcommerceDemo.Data;
using System.Web.Helpers;
using EcommerceDemo.Models;
using Newtonsoft.Json;

namespace EcommerceDemo.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public class IJsonType
        {
            public int id { get; set; }
            public int quantity { get; set; }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetInt32("roleIdSession") == 4)
            {
                var logId = HttpContext.Session.GetInt32("logIdSession");

                var customerDetails = _db.Customers.Where(p => p.login_id == logId).FirstOrDefault();

                TempData["customerArea"] = customerDetails.area;
                TempData["customerCity"] = customerDetails.city;
                TempData["customerZip"] = customerDetails.zip;
                return View();

            }

            return RedirectToRoute(new { action = "Registration", controller = "Home", area = "Visitor" });

        }

        public String PlaceOrder(String id)
        {
            System.Diagnostics.Debug.WriteLine("................" + id);

            var logId = HttpContext.Session.GetInt32("logIdSession");

            var customerID = _db.Customers.Where(c => c.login_id == logId).FirstOrDefault().id;

            Orders order = new Orders()
            {
                customer_id = customerID,
                status = 0,
                created_at = DateTime.Now,
            };

            var invokedOrder = _db.Orders.Add(order);
            _db.SaveChanges();

            var jobj = JsonConvert.DeserializeObject<IEnumerable<IJsonType>>(id);
                     
            foreach (var item in jobj)
            {
                Ordered_Products ord = new Ordered_Products()
                {
                    order_id = invokedOrder.Entity.id,
                    product_id = item.id,
                    quantity = item.quantity,
                    created_at = DateTime.Now,
                };

                _db.Ordered_Products.Add(ord);
                _db.SaveChanges();
            }           

            return "200";
        }
    }
}
