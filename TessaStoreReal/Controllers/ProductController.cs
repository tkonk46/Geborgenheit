using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TessaStoreReal.Models;


namespace TessaStoreReal.Controllers
{
    public class ProductController : Controller
    {
        private static Product product = new Product();

        // GET: Product{id}
        public ActionResult Index(int? id)
        {
            using (TessaStoreReal.Models.StorefrontEntities entities = new StorefrontEntities())

            {
                var product = entities.Products.Single(x => x.ID == id);

                Product model = new Product();
                model.Name = product.Name;
                model.Quantity = product.Quantity;
                model.Type = product.Type;
                model.Price = product.Price;
                model.ImageUrl = product.ImageUrl;
                model.Description = product.Description;
                model.DateAdded = product.DateAdded;
                model.ID = product.ID;

                return View(model);

            }
        }

        //POST: Product{id}
        [HttpPost]
        public ActionResult Index(Product product)
        {
            string name = product.Name;
            int? quantity = product.Quantity;
            string type = product.Type;
            decimal? price = product.Price;
            string imageurl = product.ImageUrl;
            string description = product.Description;
            //DateTime dateadded = product.DateAdded;
            int id = product.ID;
            using (TessaStoreReal.Models.StorefrontEntities entities = new StorefrontEntities())
            {
                Order currentOrder = null;
                if (User.Identity.IsAuthenticated)
                {
                    Customer currentCustomer = entities.Customers.Single(x => x.EmailAddress == User.Identity.Name);
                    currentOrder = currentCustomer.Orders.FirstOrDefault(x => x.DatePlaced == null);

                    if(currentOrder == null)
                    {
                        currentOrder = new Order();
                        currentCustomer.Orders.Add(currentOrder);

                    }
                }
                else
                {
                    if (Request.Cookies.AllKeys.Contains("OrderNumber"))//cartexists
                    {
                        currentOrder = null;
                        int orderId = (int.Parse(Request.Cookies["OrderNumber"].Value));
                        currentOrder = entities.Orders.FirstOrDefault(x => x.OrderNumber == orderId);
                        if(currentOrder == null)
                        {
                            currentOrder = new Order();
                            entities.Orders.Add(currentOrder);
                            entities.SaveChanges();
                            Response.Cookies.Add(new HttpCookie("OrderNumber", currentOrder.OrderNumber.ToString()));
                        }
                    }
                    else
                    {
                        currentOrder = new Order();
                        entities.Orders.Add(currentOrder);
                        entities.SaveChanges();
                        Response.Cookies.Add(new HttpCookie("OrderNumber", currentOrder.OrderNumber.ToString()));
                    }
                }


                try
                {
                    OrderProduct currentItem = currentOrder.OrderProducts.FirstOrDefault(x => x.ProductsID == product.ID);
                    if (currentItem == null)
                    {
                        currentItem = new OrderProduct { ProductsID = id, Quantity = quantity ?? 1};

                        currentOrder.OrderProducts.Add(currentItem);
                    }
                    else
                    {
                        currentItem.Quantity += quantity ?? 1;
                    }
                    entities.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw ex;//remove quantity as primary key on orderproducts+update database
                }
            }
                            
            return RedirectToAction("Index","Cart", new { name=product.Name, quantity=product.Quantity, type=product.Type, price=product.Price, imageurl=product.ImageUrl, description=product.Description, dateadded=product.DateAdded, id=product.ID  } );
        }

    }


}

