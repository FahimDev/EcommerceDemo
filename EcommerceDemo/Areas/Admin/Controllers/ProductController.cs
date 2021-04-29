using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using EcommerceDemo.Models;
using EcommerceDemo.Data;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Drawing;



namespace EcommerceDemo.Areas.Admin.Controllers
{   [Area("Admin")]
    public class ProductController : Controller
    {

        private ApplicationDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                return View(_db.Products.ToList());
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
                /*
                ---------->Note<-----------
               The VOLUME TABLE & The CATEGORY TABLE
               Both must have enough values to show up on the 
               DROPDOWN CATEGORY list in the VIEW. 

               If any Category has it's name but the UNIT is missing
               it can not be seen in the DROPDOWN LIST in the View
                [arif]
                */

                var createProduct = _db.ProductCatagories.ToList();              

                TempData["Category"] = createProduct;
                
                return View();
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }

        [HttpPost]
        public IActionResult Create(AddNewProduct products)
        {
            if (HttpContext.Session.GetInt32("roleIdSession") == 1) {
                //var createProduct = _db.ProductCatagories.Join(_db.ProductVolumes, productCat => productCat.product_volume_id, productVol => productVol.id, (productCat, productVol) => new CreateFormViewModel { catBody = productCat, catUnit = productVol }).ToList();
                var createProduct = _db.ProductCatagories.ToList();
                TempData["Category"] = createProduct;

                if (!ModelState.IsValid)
                {
                    return View();
                }

                /*
                 Must install the System.Drawing.Common NuGet package. This contains Image and other related types like Bitmap.
                   The command is given below ======>
                       PM> Install-Package System.Drawing.Common
                */
                String uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "prod");

                //------------DISPLAY IMAGE
                String displayImage = products.h_product_image.Replace("data:image/png;base64,", String.Empty);
                byte[] bytes = Convert.FromBase64String(displayImage);
                MemoryStream ms = new MemoryStream(bytes);
                Image pic = Image.FromStream(ms);
                String imageName = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                String imgPath = Path.Combine(uploadFolder, imageName);
                pic.Save(imgPath);

                //------------Image ONE

                String imageOne = products.h_image_one.Replace("data:image/png;base64,", String.Empty);
                byte[] byteOne = Convert.FromBase64String(imageOne);
                MemoryStream msOne = new MemoryStream(byteOne);
                Image picOne = Image.FromStream(msOne);
                String imageNameOne = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                String imgPathOne = Path.Combine(uploadFolder, imageNameOne);
                picOne.Save(imgPathOne);


                //------------Image TWO

                String imageTwo = products.h_image_two.Replace("data:image/png;base64,", String.Empty);
                byte[] byteTwo = Convert.FromBase64String(imageTwo);
                MemoryStream msTwo = new MemoryStream(byteTwo);
                Image picTwo = Image.FromStream(msTwo);
                String imageNameTwo = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                String imgPathTwo = Path.Combine(uploadFolder, imageNameTwo);
                picTwo.Save(imgPathTwo);


                //------------Image THREE

                String imageThree = products.h_image_three.Replace("data:image/png;base64,", String.Empty);
                byte[] byteThree = Convert.FromBase64String(imageThree);
                MemoryStream msThree = new MemoryStream(byteThree);
                Image picThree = Image.FromStream(msThree);
                String imageNameThree = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                String imgPathThree = Path.Combine(uploadFolder, imageNameThree);
                picThree.Save(imgPathThree);

                //------------Image Four

                String imageFour = products.h_image_four.Replace("data:image/png;base64,", String.Empty);
                byte[] byteFour = Convert.FromBase64String(imageFour);
                MemoryStream msFour = new MemoryStream(byteFour);
                Image picFour = Image.FromStream(msFour);
                String imageNameFour = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                String imgPathFour = Path.Combine(uploadFolder, imageNameFour);
                picFour.Save(imgPathFour);


                //-------------------------------------------------------------------------------------------

                Products product = new Products();
                product.product_name = products.product_name;
                product.catagory_id = products.category_id;
                product.company_id = products.company_id;
                product.product_img = imageName;
                product.video_url = products.video_url;
                product.product_description = products.product_description;
                product.packing_type = products.packing_type;
                product.product_material = products.product_material;
                product.product_brand = products.product_brand;
                product.product_color = products.product_color;
                product.minimum_order = products.minimum_order;
                product.product_sell = products.product_sell;
                product.product_price = products.product_price;
                product.created_at = DateTime.Now;

                var insertProd = _db.Products.Add(product);
                _db.SaveChanges();

                System.Diagnostics.Debug.WriteLine("Insert Product Table================================>" + insertProd);


                ProductImages productImageList = new ProductImages();
                productImageList.product_id = insertProd.Entity.id;
                productImageList.image1_path = imageNameOne;
                productImageList.image2_path = imageNameTwo;
                productImageList.image3_path = imageNameThree;
                productImageList.image4_path = imageNameFour;
                productImageList.created_at = DateTime.Now;

                var insertProdImage = _db.ProductImages.Add(productImageList);

                System.Diagnostics.Debug.WriteLine("Insert Product Images Table================================>" + insertProdImage);
                _db.SaveChanges();

                TempData["action"] = "Product added";

                return View();
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var categories = _db.ProductCatagories.ToList();

            TempData["Category"] = categories;

            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                var products = _db.Products.Find(id);
                var prodImg = _db.ProductImages.Where(prodImg => prodImg.product_id == id).FirstOrDefault();

                System.Diagnostics.Debug.WriteLine("........................>>>" + id);

                AddNewProduct prod = new AddNewProduct
                {
                    product_name = products.product_name,
                    category_id = products.catagory_id,
                    company_id = products.company_id,
                    video_url = products.video_url,
                    product_description = products.product_description,
                    packing_type = products.packing_type,
                    product_material = products.product_material,
                    product_brand = products.product_brand,
                    product_color = products.product_color,
                    minimum_order = products.minimum_order,
                    product_sell = products.product_sell,
                    product_price = products.product_price
                };

                return View(prod);
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }
        [HttpPost]
        public IActionResult Update(int id, AddNewProduct products)
        {
            var createProduct = _db.ProductCatagories.ToList();

            TempData["Category"] = createProduct;
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {

                Products prod = _db.Products.Find(id);
                ProductImages prodImg = _db.ProductImages.Find(id);

                String uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "prod");

                //------------DISPLAY IMAGE
                if (products.h_product_image != null)
                {
                    String displayImage = products.h_product_image.Replace("data:image/png;base64,", String.Empty);
                    byte[] bytes = Convert.FromBase64String(displayImage);
                    MemoryStream ms = new MemoryStream(bytes);
                    Image pic = Image.FromStream(ms);
                    String imageName = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                    String imgPath = Path.Combine(uploadFolder, imageName);
                    pic.Save(imgPath);
                    prod.product_img = imgPath;
                }

                //------------Image ONE
                if (products.h_image_one != null)
                {
                    String imageOne = products.h_image_one.Replace("data:image/png;base64,", String.Empty);
                    byte[] byteOne = Convert.FromBase64String(imageOne);
                    MemoryStream msOne = new MemoryStream(byteOne);
                    Image picOne = Image.FromStream(msOne);
                    String imageNameOne = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                    String imgPathOne = Path.Combine(uploadFolder, imageNameOne);
                    picOne.Save(imgPathOne);
                    prodImg.image1_path = imgPathOne;
                }

                //------------Image TWO
                if (products.h_image_two != null)
                {
                    String imageTwo = products.h_image_two.Replace("data:image/png;base64,", String.Empty);
                    byte[] byteTwo = Convert.FromBase64String(imageTwo);
                    MemoryStream msTwo = new MemoryStream(byteTwo);
                    Image picTwo = Image.FromStream(msTwo);
                    String imageNameTwo = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                    String imgPathTwo = Path.Combine(uploadFolder, imageNameTwo);
                    picTwo.Save(imgPathTwo);
                    prodImg.image2_path = imgPathTwo;
                }

                //------------Image THREE
                if (products.h_image_three != null)
                {
                    String imageThree = products.h_image_three.Replace("data:image/png;base64,", String.Empty);
                    byte[] byteThree = Convert.FromBase64String(imageThree);
                    MemoryStream msThree = new MemoryStream(byteThree);
                    Image picThree = Image.FromStream(msThree);
                    String imageNameThree = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                    String imgPathThree = Path.Combine(uploadFolder, imageNameThree);
                    picThree.Save(imgPathThree);
                    prodImg.image3_path = imgPathThree;
                }

                //------------Image Four
                if (products.h_image_four != null)
                {
                    String imageFour = products.h_image_four.Replace("data:image/png;base64,", String.Empty);
                    byte[] byteFour = Convert.FromBase64String(imageFour);
                    MemoryStream msFour = new MemoryStream(byteFour);
                    Image picFour = Image.FromStream(msFour);
                    String imageNameFour = products.product_name + "_" + Guid.NewGuid().ToString() + ".png";
                    String imgPathFour = Path.Combine(uploadFolder, imageNameFour);
                    picFour.Save(imgPathFour);
                    prodImg.image4_path = imgPathFour;
                }

                prod.product_name = products.product_name;
                prod.catagory_id = products.category_id;
                prod.company_id = products.company_id;
                prod.video_url = products.video_url;
                prod.product_description = products.product_description;
                prod.packing_type = products.packing_type;
                prod.product_material = products.product_material;
                prod.product_brand = products.product_brand;
                prod.product_color = products.product_color;
                prod.minimum_order = products.minimum_order;
                prod.product_sell = products.product_sell;
                prod.product_price = products.product_price;
                prod.updated_at = DateTime.Now;

                _db.SaveChanges();

                return RedirectToRoute(new { action = "Index", controller = "Product", area = "Admin" });
            }
            else
            {
                TempData["login_alert"] = "Please, login!";
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }
    }
}
