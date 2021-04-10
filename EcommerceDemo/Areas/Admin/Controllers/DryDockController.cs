using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceDemo.Models;
using EcommerceDemo.Data;
using System.Web.Helpers;

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
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("registrationForm")]
        public IActionResult Registration(Admins admin)
        {

            //PM> Install-Package microsoft-web-helpers
            String salt = Crypto.GenerateSalt();
            String password = "990331" + salt;
            String hashPass = Crypto.HashPassword(password);

            admin.login_id = 1;
            admin.created_at = DateTime.Now ;
            admin.updated_at = DateTime.Now;

            Logins logins = new Logins();
            logins.role_id = 1;
            logins.username = "fahim0373";
            logins.password = hashPass;
            logins.token = salt;
            logins.created_at = DateTime.Now;

            var isVerified = Crypto.VerifyHashedPassword(hashPass,"990331"+salt);
            System.Diagnostics.Debug.WriteLine("================================>"+isVerified);

            /*

            var insertLogin = _db.Logins.Add(logins);
            _db.SaveChanges();
            System.Diagnostics.Debug.WriteLine(insertLogin.Entity.id);
            */
            //-------------------------------
            //var insertReg = _db.Admins.Add(admin);
            //_db.SaveChanges();
            //-------------------------------
            //System.Diagnostics.Debug.WriteLine(admin.contact+admin.email);
            return View();
        }
    }
}
