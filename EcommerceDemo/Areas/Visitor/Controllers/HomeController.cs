using EcommerceDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EcommerceDemo.Data;
using Microsoft.AspNetCore.Http;

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

        public IActionResult CategoryCollections() {

            return View(_db.ProductCatagories.ToList());
        }

        public IActionResult CategoryProductCollection(int id)
        {
            var prod = _db.Products.Where(p => p.catagory_id == id).ToList();

            List <int> newProd  = new List<int>();
            foreach (var item in prod)
            {
                if ((DateTime.Now - item.created_at).Days <= 30)
                {
                    newProd.Add(item.id);
                }
            }

            //(DateTime.Now - p.created_at).Days

            System.Diagnostics.Debug.WriteLine(newProd[0]);
            return View();
        }

        public IActionResult Details(int id)
        {            
            ProductJoinCatagory productDetails = new ProductJoinCatagory();
    
            var bara = _db.Products.Join(_db.ProductCatagories, product => product.catagory_id , productCat => productCat.id, (product, productCat) 
                => new { product_id = product.id, catagory_name = productCat.catagory_name, product_name = product.product_name , 
                    product_details = product.product_description, catagory_policy = productCat.policy, product_image = product.product_img, 
                    video_url = product.video_url, packing_type = product.packing_type, product_material = product.product_material, 
                    product_brand = product.product_brand, product_price = product.product_price, produc_sell = product.product_sell, minimum_order = product.minimum_order , volume_id = productCat.product_volume_id } ).FirstOrDefault();

            var chad = _db.ProductVolumes.Where(v => v.id == bara.volume_id).FirstOrDefault();

            var uthechiloGoGoNe = _db.ProductImages.Where(img => img.product_id == bara.product_id).FirstOrDefault();

            var ratings = _db.Reviews.Where(review => review.product_id == bara.product_id).ToList() ;

            //---------------------- AVG of Ratings
            float sum = 0;
            foreach (var item in ratings)
            {
                sum += item.rating;
            }
            sum = sum / ratings.Count;
            //----------------------

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
            productDetails.small = chad.small;
            productDetails.medium = chad.medium;
            productDetails.large = chad.large;
            productDetails.unit = chad.unit;
            productDetails.image1_path = uthechiloGoGoNe.image1_path;
            productDetails.image2_path = uthechiloGoGoNe.image2_path;
            productDetails.image3_path = uthechiloGoGoNe.image3_path;
            productDetails.image4_path = uthechiloGoGoNe.image4_path;
            productDetails.reviews = ratings;
            productDetails.rating = sum;
            productDetails.reviewers = ratings.Count;



            //System.Diagnostics.Debug.WriteLine("--------------------->" + ratings[0].review_body);
            System.Diagnostics.Debug.WriteLine("--------------------->" + HttpContext.Session.GetString("userSession"));
           
            return View(productDetails);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
