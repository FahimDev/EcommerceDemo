using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EcommerceDemo.Data;

namespace EcommerceDemo.Areas.Customer.Controllers
{

    [Area("Customer")]

    public class HomeController : Controller
    {

        private ApplicationDbContext _db;


        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
           
            if (HttpContext.Session.GetInt32("roleIdSession") == 4)
            {

                var loginId = HttpContext.Session.GetInt32("logIdSession");

                var customerInfo = _db.Customers.Where(cus => cus.login_id == loginId).FirstOrDefault();

                return View(customerInfo);
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }

        }
    }
}
