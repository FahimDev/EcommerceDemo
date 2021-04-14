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
{   [Area("Admin")]
    public class ProductController : Controller
    {

        private ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                
                var createProduct = _db.ProductCatagories.Join(_db.ProductVolumes, productCat => productCat.product_volume_id, productVol => productVol.id, (productCat, productVol) => new CreateFormViewModel { catBody = productCat, catUnit = productVol }).ToList();
                /*
                 ---------->Note<-----------
                The VOLUME TABLE & The CATEGORY TABLE
                Both must have enough values to show up on the 
                DROPDOWN CATEGORY list in the VIEW. 

                If any Category has it's name but the UNIT is missing
                it can not be seen in the DROPDOWN LIST in the View
                 */
                
                return View(createProduct);
            }
            else
            {
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }

        [HttpPost]
        public IActionResult Create(Products products)
        {
            return View();
        }
    }
}
