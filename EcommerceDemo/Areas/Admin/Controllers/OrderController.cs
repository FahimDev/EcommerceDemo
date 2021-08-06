using EcommerceDemo.Data;
using EcommerceDemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;
 
        public OrderController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {          
            var orderedDetails = _db.Orders.Join(_db.Ordered_Products, o => o.id, op => op.order_id, (o, op) => new { o, op })
                .Join(_db.Products, oop => oop.op.product_id, p => p.id, (oop, p) => new{ oop, p})
                .Join(_db.Customers, oopp => oopp.oop.o.customer_id, c => c.id, (oopp, c) => new {
                    id = oopp.oop.o.id,
                    total_product_price = oopp.p.product_price * oopp.oop.op.quantity,
                    quantity = oopp.oop.op.quantity,
                    created_at = oopp.oop.o.created_at,
                    ordered_by = c.full_name,
                    customer_id = c.id,
                })
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

            //var jobj = JsonConvert.SerializeObject(orderedProducts);

            //System.Diagnostics.Debug.WriteLine("................" + jobj);
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
            
            return View(datas);
        }
    }
}
