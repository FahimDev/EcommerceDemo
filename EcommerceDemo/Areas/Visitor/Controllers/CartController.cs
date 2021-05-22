using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceDemo.Models;
using EcommerceDemo.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Dynamic;

namespace EcommerceDemo.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    public class CartController : Controller
    {
        private ApplicationDbContext _db;
        //private Context _context;
        public class IJsonType
        {
            public int id { get; set; }
            public int quantity { get; set; }
        }
        public class ProductsExtend : Products
        {
            public int quantity { get; set; }
        }
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public dynamic GetProductDetails(string id)
        {
            

            System.Diagnostics.Debug.WriteLine("................" + id);
            var jobj = JsonConvert.DeserializeObject<IEnumerable<IJsonType>>(id);
            int total = 0;
            List<ProductsExtend> products = new List<ProductsExtend>();
            foreach (var item in jobj)
            {
                Products product =  _db.Products.Where(p => p.id == item.id).FirstOrDefault();
                ProductsExtend pe = new ProductsExtend
                {
                    id = product.id,
                    product_img = product.product_img,
                    product_name = product.product_name,
                    product_price = product.product_price,
                    quantity = item.quantity,
                };

                total += product.product_price * item.quantity;
                
                products.Add(pe);
                //System.Diagnostics.Debug.WriteLine(item.id);
            }

            dynamic cartDetails = new ExpandoObject();
            cartDetails.products = products;
            cartDetails.total = total;

            return cartDetails;
        }
    }
}
