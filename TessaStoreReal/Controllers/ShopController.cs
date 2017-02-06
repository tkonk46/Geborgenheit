using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TessaStoreReal.Models;

namespace TessaStoreReal.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            using (TessaStoreReal.Models.StorefrontEntities entities = new Models.StorefrontEntities())
            {
                var product = entities.Products.ToArray();


                return View(product);
                //add images to content, store urls in database
            };


        }

        [HttpPost]
        public ActionResult Index(Product product)
        {
            using (TessaStoreReal.Models.StorefrontEntities entities = new Models.StorefrontEntities())
            {
                //string name = product.Name;
                //int? quantity = product.Quantity;
                //string type = product.Type;
                //decimal? price = product.Price;
                //string imageurl = product.ImageUrl;
                //string description = product.Description;
                //DateTime dateadded = product.DateAdded;
                int id = product.ID;

                return RedirectToAction("Index", "Product", new { id = product.ID });
            };
        }
    }
}