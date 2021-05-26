using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EcommerceDemo.Data;
using System.Web.Helpers;
using EcommerceDemo.Models;

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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {

            var logId = HttpContext.Session.GetInt32("logIdSession");

            var customerDetails = _db.Customers.Where(p => p.login_id == logId).FirstOrDefault();

            TempData["customerArea"] = customerDetails.area;
            TempData["customerCity"] = customerDetails.city;
            TempData["customerZip"] = customerDetails.zip;

            return View();
        }
    }
}
