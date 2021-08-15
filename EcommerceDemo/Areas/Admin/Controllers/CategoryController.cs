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
using System.Drawing;


namespace EcommerceDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private ApplicationDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;
        //[System.Drawing.dll]
        public CategoryController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {         
            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                var categories = _db.ProductCatagories.ToList();
                return View(categories);
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }

        public IActionResult Create()
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

        [HttpPost]
        public IActionResult Create(CategoryCreateView categoryCreate)
        {

            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                System.Diagnostics.Debug.WriteLine("================================>" + categoryCreate.imageblob);
                
                String convert = categoryCreate.imageblob.Replace("data:image/png;base64,", String.Empty);//Cover

                String convertBanner = categoryCreate.imageblob_banner.Replace("data:image/png;base64,", String.Empty);//Homepage Banner


                String uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "banner");

                //Cover Photo
                byte[] bytes = Convert.FromBase64String(convert);
                MemoryStream ms = new MemoryStream(bytes);
                
                Image pic = Image.FromStream(ms);
                //_____________________________________________

                //Banner Photo

                byte[] banner_bytes = Convert.FromBase64String(convertBanner);
                MemoryStream banner_ms = new MemoryStream(banner_bytes);

                Image banner_pic = Image.FromStream(banner_ms);


                /*
                  Must install the System.Drawing.Common NuGet package. This contains Image and other related types like Bitmap.
                    The command is given below ======>
                        PM> Install-Package System.Drawing.Common
                 */

                //Cover Photo
                String imageName = Guid.NewGuid().ToString() + "_" + "dasda.png";

                String imgPath = Path.Combine(uploadFolder, imageName);

                pic.Save(imgPath);

                //_____________________________________________

                //Banner Photo
                String bannerImageName = Guid.NewGuid().ToString() + "_" + "dasdaHome.png";

                String bannerImgPath = Path.Combine(uploadFolder, bannerImageName);

                banner_pic.Save(bannerImgPath);

                //categoryCreate.category_image.CopyTo(new FileStream(imgPath, FileMode.Create));

                ProductCatagories prodCat = new ProductCatagories();
               // prodCat.product_volume_id = catVol_id;
                prodCat.catagory_name = categoryCreate.category_name;
                prodCat.catagory_img_path = imageName;
                prodCat.banner_img_path = bannerImageName;
                prodCat.policy = categoryCreate.category_policy;
                prodCat.created_at = DateTime.Now;
                prodCat.unit = categoryCreate.category_unit;
                prodCat.small = categoryCreate.volume_small;
                prodCat.medium = categoryCreate.volume_medium;
                prodCat.large = categoryCreate.volume_large;
                var insertCat = _db.ProductCatagories.Add(prodCat);
                _db.SaveChanges();

                TempData["action"] = "Category created";

                return View();
            }
            else
            {
                TempData["login_alert"] = "Please, login!";

                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }

        }

        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                var data = _db.ProductCatagories.Find(id);
                //var data = _db.ProductCatagories.Where(prod => prod.id == id).FirstOrDefault();
                System.Diagnostics.Debug.WriteLine("........................" + id);
                CategoryCreateView category = new CategoryCreateView();
                category.id = data.id;
                category.category_name = data.catagory_name;
                category.category_unit = data.unit;
                category.category_policy = data.policy;
                category.category_img_path = data.catagory_img_path;
                category.volume_small = data.small;
                category.volume_medium = data.medium;
                category.volume_large = data.large;
                return View(category);
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }
        [HttpPost]
        public IActionResult Update(int id, CategoryCreateView categoryUpdate)
        {
            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {     
                ProductCatagories prodCat = _db.ProductCatagories.Find(id);
                // prodCat.product_volume_id = catVol_id;
                if(categoryUpdate.imageblob != null)
                {
                    String convert = categoryUpdate.imageblob.Replace("data:image/png;base64,", String.Empty);
                    String uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "banner");

                    byte[] bytes = Convert.FromBase64String(convert);
                    MemoryStream ms = new MemoryStream(bytes);
                    Image pic = Image.FromStream(ms);
                    String imageName = Guid.NewGuid().ToString() + "_" + "dasda.png";
                    String imgPath = Path.Combine(uploadFolder, imageName);

                    pic.Save(imgPath);
                    prodCat.catagory_img_path = imageName;
                }

                prodCat.catagory_name = categoryUpdate.category_name;
                prodCat.policy = categoryUpdate.category_policy;
                prodCat.created_at = DateTime.Now;
                prodCat.unit = categoryUpdate.category_unit;
                prodCat.small = categoryUpdate.volume_small;
                prodCat.medium = categoryUpdate.volume_medium;
                prodCat.large = categoryUpdate.volume_large;
                
                _db.SaveChanges();

                return RedirectToRoute(new { action = "Index", controller = "Category", area = "Admin" });

            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }
    }
}
