using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Areas.Visitor.Controllers
{
    public class LoginController : Controller
    {
        [Area("Visitor")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["login_alert"] = "Successfully Logged Out!";
            return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
        }
    }
}
