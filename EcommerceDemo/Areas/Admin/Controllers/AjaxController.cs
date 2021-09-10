using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using EcommerceDemo.Models;
using EcommerceDemo.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EcommerceDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class AjaxController : Controller
    {
        private ApplicationDbContext _db;
        public AjaxController(ApplicationDbContext db)
        {
            _db = db;
        }

        public class DashboardData
        {
            public int total_order { get; set; }
            public int total_customer { get; set; }
            public int total_sell { get; set; }
        }
        

        public DashboardData GetDashboardData()
        {
            DashboardData dashData = new DashboardData();
            var todaysDate = DateTime.Now.Day;
            var total_order = _db.Orders.Where(o => o.created_at.Day == todaysDate);
            var total_customer = _db.Logins.Where(l => l.created_at.Day == todaysDate).Where(l => l.role_id == 4);
            var total_sell = _db.Ordered_Products.Where(l => l.created_at.Day == todaysDate).Join(_db.Products, op => op.product_id, p => p.id, (op,p) => new {
                product_id = p.id,
                product_quantity = op.quantity,
                product_price = p.product_price,
                price_total = p.product_price * op.quantity,
            }).Sum(opp => opp.price_total);

                //Where(l => l.created_at.Day == todaysDate).Where(l => l.role_id == 4).Count();
            dashData.total_order = total_order.Count();
            dashData.total_customer = total_customer.Count();
            dashData.total_sell = total_sell;
            return dashData;
        }
    }
}
