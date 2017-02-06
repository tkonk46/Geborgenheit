using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using TessaStoreReal.Models;

namespace TessaStoreReal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        //POST: Login
        [HttpPost]
        public ActionResult Index(LoginModel model, string returnUrl="/")
        {
            if(ModelState.IsValid)
            {
               if(WebSecurity.Login(model.EmailAddress, model.Password, true))
                {
                    if (Request.Cookies.AllKeys.Contains("OrderNumber"))//cartexists
                    {
                        using (StorefrontEntities entities = new StorefrontEntities())
                        {
                            Order currentOrder = null;
                            int orderId = (int.Parse(Request.Cookies["OrderNumber"].Value));
                            currentOrder = entities.Orders.FirstOrDefault(x => x.OrderNumber == orderId);
                            if (currentOrder != null)
                            {
                                currentOrder.CustomerID = entities.Customers.Single(x => x.EmailAddress == model.EmailAddress).CustomerID;
 
                                entities.SaveChanges();
                                Response.Cookies.Add(new HttpCookie("OrderNumber", "") { Expires = DateTime.UtcNow });
                            }
                        }
                    }

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("EmailAddress", "Username or password was incorrect.");
            }
            return View(model);
        }
    }
}