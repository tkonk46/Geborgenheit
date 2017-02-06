using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TessaStoreReal.Models;
using WebMatrix.WebData;

namespace TessaStoreReal.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new ForgotPasswordModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(ForgotPasswordModel model)
        {
            if (WebSecurity.UserExists(model.EmailAddress))
            {
                string resetToken = WebSecurity.GeneratePasswordResetToken(model.EmailAddress);
                string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/ForgotPassword/Reset?resetToken=" + resetToken;

                string sendGridApiKey = ConfigurationManager.AppSettings["SendGridApiKey"];

                SendGrid.SendGridAPIClient client = new SendGrid.SendGridAPIClient(sendGridApiKey);

                Email from = new Email("admin@geborgenheit.com");
                string subject = "Reset Password Request";
                Email to = new Email(model.EmailAddress);
                Content content = new Content("text/html", string.Format("<a href=\"{0}\">Reset your password</a>", resetUrl));
                Mail mail = new Mail(from, subject, to, content);     

                var response = await client.client.mail.send.post(requestBody: mail.Get());
                
                string message = await response.Body.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(message))
                {
                    throw new Exception(message);
                }

            }
            return RedirectToAction("ResetSent");
        }
        [AllowAnonymous]
        public ActionResult ResetSent()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Reset(string resetToken)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.ResetToken = resetToken;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Reset(ResetPasswordModel model)
            {
            if(ModelState.IsValid)
            {
                WebSecurity.ResetPassword(model.ResetToken, model.Password);
                return RedirectToAction("Index","Login");
            }
            return View(model);
        }
    }
}