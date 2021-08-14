using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EcommerceDemo.Data;
using EcommerceDemo.Models;


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

                var orderedDetails = _db.Orders.Join(_db.Ordered_Products, o => o.id, op => op.order_id, (o, op) => new { o, op })
                .Join(_db.Products, oop => oop.op.product_id, p => p.id, (oop, p) => new { oop, p })
                .Join(_db.Customers, oopp => oopp.oop.o.customer_id, c => c.id, (oopp, c) => new {
                    id = oopp.oop.o.id,
                    total_product_price = oopp.p.product_price * oopp.oop.op.quantity,
                    quantity = oopp.oop.op.quantity,
                    created_at = oopp.oop.o.created_at,
                    ordered_by = c.full_name,
                    customer_id = c.id,
                })
                .Where(all => all.customer_id == loginId)
                .GroupBy(all => new
                {
                    id = all.id,
                    created_at = all.created_at,
                    ordered_by = all.ordered_by,
                    customer_id = all.customer_id,
                })
                .Select(all => new
                {
                    id = all.Key.id,
                    total_price = all.Sum(all => all.total_product_price),
                    total_quantity = all.Sum(all => all.quantity),
                    created_at = all.Key.created_at,
                    ordered_by = all.Key.ordered_by,
                    custormer_id = all.Key.customer_id,
                }).ToList();

                List<OrderExtended> datas = new List<OrderExtended>();
                foreach (var order in orderedDetails)
                {
                    OrderExtended ordExt = new OrderExtended()
                    {
                        id = order.id,
                        total_price = order.total_price,
                        total_quantity = order.total_quantity,
                        ordered_by = order.ordered_by,
                        created_at = order.created_at,
                        customer_id = order.custormer_id,
                    };

                    datas.Add(ordExt);
                }

                CustomerHome customerHomeData = new CustomerHome() {
                    custormerInfo = customerInfo,
                    orders = datas,
                };


                return View(customerHomeData);
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }

        }
    }
}
