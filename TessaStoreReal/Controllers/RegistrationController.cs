using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TessaStoreReal.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            Models.RegistrationModel model = new Models.RegistrationModel();
            return View(model);
        }

        //POST: Registration
        [HttpPost]
        public ActionResult Index(Models.RegistrationModel model)
        {
            if(ModelState.IsValid)
            {
                if(WebMatrix.WebData.WebSecurity.UserExists(model.EmailAddress))
                    {
                         ModelState.AddModelError("EmailAddress", "User account has already been created.");
                    }
                else
                {
                    WebMatrix.WebData.WebSecurity.CreateUserAndAccount(model.EmailAddress, model.Password, null, false);
                    WebMatrix.WebData.WebSecurity.Login(model.EmailAddress, model.Password, true);
                    return RedirectToAction("Index", "Home");

                }
            }
            return View(model);
        }
    }
}