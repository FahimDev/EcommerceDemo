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
            return View();
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

                return View();
            }
            else
            {
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }
    }
}
