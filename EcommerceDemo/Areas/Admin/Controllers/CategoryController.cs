﻿using Microsoft.AspNetCore.Mvc;
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
    public class CategoryController : Controller
    {

        private ApplicationDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;

        public CategoryController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
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
                return View();
            }
            else
            {
                return RedirectToRoute(new { action = "Index", controller = "Home", area = "Visitor" });
            }
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateView categoryCreate)
        {

            if (HttpContext.Session.GetInt32("roleIdSession") == 1)
            {
                String uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "banner");

                String imageName = Guid.NewGuid().ToString() + "_" + categoryCreate.category_image.FileName;

                String imgPath = Path.Combine(uploadFolder, imageName);

                categoryCreate.category_image.CopyTo(new FileStream(imgPath, FileMode.Create));

                ProductVolumes prodVol = new ProductVolumes();
                prodVol.unit = categoryCreate.category_unit;
                prodVol.small = categoryCreate.volume_small;
                prodVol.medium = categoryCreate.volume_medium;
                prodVol.large = categoryCreate.volume_large;
                prodVol.created_at = DateTime.Now;


                var insertVol = _db.ProductVolumes.Add(prodVol);
                _db.SaveChanges();

                int catVol_id = insertVol.Entity.id;
                ProductCatagories prodCat = new ProductCatagories();
                prodCat.product_volume_id = catVol_id;
                prodCat.catagory_name = categoryCreate.category_name;
                prodCat.catagory_img_path = imageName;
                prodCat.policy = categoryCreate.category_policy;
                prodCat.created_at = DateTime.Now;

                var insertCat = _db.ProductCatagories.Add(prodCat);
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