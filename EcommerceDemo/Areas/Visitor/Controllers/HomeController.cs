using EcommerceDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EcommerceDemo.Data;

namespace EcommerceDemo.Controllers
{
    [Area("Visitor")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        //private Context _context;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        private readonly ILogger<HomeController> _logger;
        /*
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        */

        public IActionResult Index()
        {
            return View(_db.Products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {            
            ProductJoinCatagory productDetails = new ProductJoinCatagory();
    
            var bara = _db.Products.Join(_db.ProductCatagories, product => product.catagory_id , productCat => productCat.id, (product, productCat) 
                => new { product_id = product.id, catagory_name = productCat.catagory_name, product_name = product.product_name , 
                    product_details = product.product_description, catagory_policy = productCat.policy, product_image = product.product_img, 
                    video_url = product.video_url, packing_type = product.packing_type, product_material = product.product_material, 
                    product_brand = product.product_brand, product_price = product.product_price, produc_sell = product.product_sell, minimum_order = product.minimum_order } ).FirstOrDefault();

            productDetails.product_id = bara.product_id;
            productDetails.product_name = bara.product_name;
            productDetails.catagory_name = bara.catagory_name;
            productDetails.product_details = bara.product_details;
            productDetails.catagory_policy = bara.catagory_policy;
            productDetails.product_image = bara.product_image;
            productDetails.video_url = bara.video_url;
            productDetails.packing_type = bara.packing_type;
            productDetails.product_material = bara.product_material;
            productDetails.product_brand = bara.product_brand;
            productDetails.product_price = bara.product_price;
            productDetails.produc_sell = bara.produc_sell;
            productDetails.minimum_order = bara.minimum_order;
            //System.Diagnostics.Debug.WriteLine(data.ToString());
            return View(productDetails);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
