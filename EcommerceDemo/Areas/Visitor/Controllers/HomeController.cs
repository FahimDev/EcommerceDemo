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
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Web.Helpers;

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

            var getAllCat = _db.Products.Join(_db.ProductCatagories, products =>
            products.catagory_id, prodCat => prodCat.id, (products, prodCat)=> 
            new { products, prodCat }).GroupBy( p => new { catID = p.products.catagory_id, name = p.prodCat.catagory_name ,img = p.prodCat.catagory_img_path  } ).Select(
                prod =>  new  { count = prod.Count(), name = prod.Key.name, image = prod.Key.img , catId = prod.Key.catID}).ToList();

            List<ShowAllCategories> showCat = new List<ShowAllCategories>();

            foreach (var item in getAllCat)
            {
                ShowAllCategories sc = new ShowAllCategories
                {
                    category_name = item.name,
                    category_img = item.image,
                    product_count = item.count,
                    id = item.catId,
                };
                showCat.Add(sc);
            }

            //System.Diagnostics.Debug.WriteLine(getAllCat[0].name);

            return View(showCat);
        }

        
        public IActionResult CategoryProductCollection(int id)
        {
            var firstDay = DateTime.Today.AddDays(-30);
            //-----> SQL for last 30 days data ===> .Where( p => p.created_at >= firstDay)

            var prod = _db.Products.Where(p => p.catagory_id == id).ToList();

            var products = _db.Products.Where(p => p.catagory_id == id).ToList();

            List<ViewProductsByCat> viewContent = new List<ViewProductsByCat>();

            foreach (var item in products)
            {
                var reviews = _db.Reviews.Where(r => r.product_id == item.id).ToList();

                float tempRev = 0;

                if (reviews != null)
                {
                    tempRev = reviews.Average(rev => rev.rating);
                }
                else
                {
                    tempRev = 0;
                }

                bool newProdTemp;

                if ((DateTime.Now - item.created_at).Days <= 30)
                {
                    newProdTemp = true;
                }
                else
                {
                    newProdTemp = false;
                }
                ViewProductsByCat viewAsset = new ViewProductsByCat
                {
                    prodId = item.id,
                    prodName = item.product_name,
                    rating = tempRev,
                    price = item.product_price,

                    prodImage = item.product_img,

                    best = false,
                    hot = false,
                    sale = item.product_sell,
                    newProd = newProdTemp,

                };

                viewContent.Add(viewAsset);
            }


            //var json = new JavaScriptSerializer().Serialize(viewContent);
            System.Diagnostics.Debug.WriteLine("--------------------->>>>>"+viewContent[1].rating);


                return View(viewContent);
        }

        public IActionResult Details(int id)
        {            
            ProductJoinCatagory productDetails = new ProductJoinCatagory();
    
            var bara = _db.Products.Join(_db.ProductCatagories, product => product.catagory_id , productCat => productCat.id, (product, productCat) 
                => new { 
                    product_id = product.id,
                    product_name = product.product_name,
                    product_details = product.product_description,
                    product_image = product.product_img,
                    video_url = product.video_url,
                    packing_type = product.packing_type,
                    product_material = product.product_material,
                    product_brand = product.product_brand,
                    product_price = product.product_price,
                    produc_sell = product.product_sell,
                    minimum_order = product.minimum_order,
                    catagory_name = productCat.catagory_name, 
                    catagory_policy = productCat.policy, 
                    small = productCat.small,
                    medium = productCat.medium,
                    large = productCat.large,
                    unit = productCat.unit,
                    } ).Where(product => product.product_id == id).FirstOrDefault();

            //var chad = _db.ProductVolumes.Where(v => v.id == bara.volume_id).FirstOrDefault();

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
            productDetails.small = bara.small;
            productDetails.medium = bara.medium;
            productDetails.large = bara.large;
            productDetails.unit = bara.unit;
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

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(CustomerRegistration request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            String salt = Crypto.GenerateSalt();
            String password = request.password + salt;
            String hashPass = Crypto.HashPassword(password);

            Logins loginObj = new Logins();

            loginObj.username = request.username;
            loginObj.password = hashPass;
            loginObj.token = salt;
            loginObj.role_id = 4;
            loginObj.created_at = DateTime.Now;

            var insertLogin = _db.Logins.Add(loginObj);
            _db.SaveChanges();

            Customers customerObj = new Customers();
            customerObj.login_id = insertLogin.Entity.id;
            customerObj.created_at = DateTime.Now;
            customerObj.full_name = request.full_name;
            customerObj.contact = request.contact;
            customerObj.email = request.email;
            customerObj.area = request.area;
            customerObj.city = request.city;
            customerObj.zip = request.zip;
            customerObj.social_media_link = request.social_media_link;
            _db.Customers.Add(customerObj);
            _db.SaveChanges();

            TempData["action"] = "Registration Successful";

            return View();
        }
    }
}
