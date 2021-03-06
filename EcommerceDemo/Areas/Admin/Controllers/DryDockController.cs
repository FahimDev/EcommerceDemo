using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceDemo.Models;
using EcommerceDemo.Data;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;

namespace EcommerceDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DryDockController : Controller
    {
        private ApplicationDbContext _db;

        public DryDockController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                return View();
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }
        public IActionResult Registration()
        {
            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                
                var roles = _db.EmpRoles.ToList();
                //System.Diagnostics.Debug.WriteLine(roles[0].role_name);
                TempData["Roles"] = roles;
                
                            
                return View();
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
            
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("registrationForm")]
        public IActionResult Registration(AdminRegistration admin)
        {

            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {

                var roles = _db.EmpRoles.ToList();

                TempData["Roles"] = roles;

                if (!ModelState.IsValid)
                {
                    return View();
                }

                //PM> Install-Package microsoft-web-helpers
                String salt = Crypto.GenerateSalt();
                String password = admin.password + salt;
                String hashPass = Crypto.HashPassword(password);

                Logins logins = new Logins();
                logins.role_id = 1;
                logins.username = admin.username;
                logins.password = hashPass;
                logins.token = salt;
                logins.created_at = DateTime.Now;

                var isVerified = Crypto.VerifyHashedPassword(hashPass, admin.password + salt);
                System.Diagnostics.Debug.WriteLine("================================>" + isVerified);



                var insertLogin = _db.Logins.Add(logins);
                _db.SaveChanges();
                //System.Diagnostics.Debug.WriteLine(insertLogin.Entity.id);

                //-------------------------------

                Admins admins = new Admins();
                admins.login_id = insertLogin.Entity.id;
                admins.created_at = DateTime.Now;
                admins.full_name = admin.full_name;
                admins.contact = admin.contact;
                admins.email = admin.email;
                admins.location = admin.location;

                var insertReg = _db.Admins.Add(admins);
                _db.SaveChanges();
                //-------------------------------

                return RedirectToRoute(new { action = "Index", controller = "DryDock", area = "Admin" });
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }

        }

        [HttpPost]
        public IActionResult Login(Login login)
        {

            var loginData = _db.Logins.Where(user => user.username == login.username).FirstOrDefault();

            if (loginData == null)
            {
                TempData["login_alert"] = "User not found!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }

            String hashPass = loginData.password;
            String salt = loginData.token;

            var isVerified = Crypto.VerifyHashedPassword(hashPass, login.password + salt);

            if (isVerified)
            {
                System.Diagnostics.Debug.WriteLine("================================>" + isVerified);

                HttpContext.Session.SetString("userSession", loginData.username);
                HttpContext.Session.SetInt32("logIdSession", loginData.id);
                HttpContext.Session.SetInt32("roleIdSession", loginData.role_id);

                HttpContext.Session.GetString("userSession");

                if (loginData.role_id == 4)
                {

                    return RedirectToRoute(new { action = "Index", controller = "Home", area = "Customer" });

                }
                else if (loginData.role_id == 1)
                {

                    return RedirectToRoute(new { action = "Index", controller = "DryDock", area = "Admin" });

                }
                else
                {
                    TempData["login_alert"] = "Wrong Password!";
                    return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
                }
            }
            else
            {
                TempData["login_alert"] = "Wrong Password!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }           
        }

    }
}
