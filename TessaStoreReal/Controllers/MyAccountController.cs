using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TessaStoreReal.Models;

namespace TessaStoreReal.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        [Authorize]
        public ActionResult Index()
        {
            
            MyAccountModel model = new MyAccountModel();

            return View(model);
        }
    }
}