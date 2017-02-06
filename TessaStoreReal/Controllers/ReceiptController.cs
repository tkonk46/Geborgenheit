using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TessaStoreReal.Models;

namespace TessaStoreReal.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        public ActionResult Index()
        {
            ReceiptModel model = new ReceiptModel();

            using (StorefrontEntities entities = new StorefrontEntities())
            {
                int orderNumber = int.Parse(Request.Cookies["OrderNumber"].Value);
                //var order = entities.Orders.Single(x => x.OrderNumber == orderNumber);
                orderNumber = model.OrderNumber;


            }

            return View(model);
        }
    }
}