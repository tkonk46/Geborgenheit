using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TessaStoreReal.Models;

namespace TessaStoreReal.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()

        {
            CheckoutModel model = new CheckoutModel();

            using (StorefrontEntities entities = new Models.StorefrontEntities())
            {
                int orderNumber = int.Parse(Request.Cookies["OrderNumber"].Value);
                var order = entities.Orders.Single(x => x.OrderNumber == orderNumber);
                model.FirstName = order.FirstName;
                model.LastName = order.LastName;
                model.EmailAddress=order.EmailAddress;
                model.CreditCardVerificationValue = order.CreditCardVerificationValue;
                model.CreditCardExpirationMonth = order.CreditCardExpirationMonth;
                model.CreditCardExpirationYear = order.CreditCardExpirationYear;
                model.CreditCardNumber = order.CreditCardNumber;
                model.CreditCardName = order.CreditCardName;
                model.Products = order.OrderProducts.Select(x => new OrderProductModel
                {
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price=x.Product.Price

                }).ToArray();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutModel model)
        {

            using (StorefrontEntities entities = new Models.StorefrontEntities())
            {
                int orderNumber = int.Parse(Request.Cookies["OrderNumber"].Value);
                var o = entities.Orders.Single(x => x.OrderNumber == orderNumber);
                model.Products = o.OrderProducts.Select(x => new OrderProductModel
                {
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Product.Price

                }).ToArray();
                if (ModelState.IsValid)
            {
                bool addressValidationSuccessful = true;

                string smartyStreetsClientAuthId = System.Configuration.ConfigurationManager.AppSettings["SmartyStreets.AuthId"];
                string smartyStreetsAuthToken = System.Configuration.ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];

                Rentler.SmartyStreets.SmartyStreetsClient client = new Rentler.SmartyStreets.SmartyStreetsClient(smartyStreetsClientAuthId, smartyStreetsAuthToken);
                var addresses = await client.GetStreetAddressAsync(model.ShippingStreet1, null, model.ShippingStreet2, model.ShippingCity, model.ShippingState, model.ShippingZip);
                if (addresses.Count() == 0)
                {
                    ModelState.AddModelError("ShippingStreet1", "Could not find similar address.");
                    addressValidationSuccessful = false;
                } 
                else
                {
                    if (!string.IsNullOrEmpty(model.ShippingStreet1) && addresses.First().delivery_line_1 != model.ShippingStreet1)
                    {
                        ModelState.AddModelError("ShippingStreet1", string.Format("Verify Updated Address: {0}", addresses.First().delivery_line_1));
                        addressValidationSuccessful = false;
    
                    }
                    if (!string.IsNullOrEmpty(model.ShippingStreet2) && addresses.First().delivery_line_2 != model.ShippingStreet2)
                    {
                        ModelState.AddModelError("ShippingStreet2", string.Format("Verify Updated Address: {0}", addresses.First().delivery_line_2));
                        addressValidationSuccessful = false;
                    }
                    if (!string.IsNullOrEmpty(model.ShippingCity)&& addresses.First().components.city_name != model.ShippingCity)
                    {
                        ModelState.AddModelError("ShippingCity", string.Format("Verify Updated Address: {0}", addresses.First().components.city_name));
                        addressValidationSuccessful = false;
                    }
                     if (!string.IsNullOrEmpty(model.ShippingState) && addresses.First().components.state_abbreviation != model.ShippingState)
                    {
                        ModelState.AddModelError("ShippingState", string.Format("Verify Updated Address: {0}", addresses.First().components.state_abbreviation));
                        addressValidationSuccessful = false;
                    }
                    if (!string.IsNullOrEmpty(model.ShippingZip) && addresses.First().components.zipcode != model.ShippingZip)
                    {
                        ModelState.AddModelError("ShippingZip", string.Format("Verify Updated Address: {0}", addresses.First().components.zipcode));
                        addressValidationSuccessful = false;
                    }
                }

                if (addressValidationSuccessful)
                {
                        string publicKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
                        string privateKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                        string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
                        string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantId"];
                        
                        Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
                        Braintree.CustomerRequest request = new Braintree.CustomerRequest();
                        request.Email = model.EmailAddress;
                        request.FirstName = model.FirstName;
                        request.LastName = model.LastName;
                        request.Phone = model.PhoneNumber;
                        request.CreditCard = new Braintree.CreditCardRequest();
                        request.CreditCard.Number = model.CreditCardNumber;
                        request.CreditCard.CardholderName = model.CreditCardName;
                        request.CreditCard.ExpirationMonth = (model.CreditCardExpirationMonth).ToString().PadLeft(2,'0');
                        request.CreditCard.ExpirationYear = model.CreditCardExpirationYear.ToString();

                        var customerResult = braintree.Customer.Create(request);
                        Braintree.TransactionRequest sale = new Braintree.TransactionRequest();
                        sale.Amount = o.OrderProducts.Sum(x => x.Product.Price ?? 0 * x.Quantity);
                        sale.CustomerId = customerResult.Target.Id;
                        sale.PaymentMethodToken = customerResult.Target.DefaultPaymentMethod.Token;
                        braintree.Transaction.Sale(sale);

                        braintree.Customer.Create(request);

                        o.FirstName = model.FirstName;
                        o.LastName = model.LastName;
                        o.EmailAddress = model.EmailAddress;
                        o.PhoneNumber = model.PhoneNumber;
                        o.ShippingStreet1 = model.ShippingStreet1;
                        o.ShippingStreet2 = model.ShippingStreet2;
                        o.ShippingCity = model.ShippingCity;
                        o.ShippingState = model.ShippingState;
                        o.ShippingZip = model.ShippingZip;

                        entities.SaveChanges();

                    return RedirectToAction("Index", "Receipt", null);
                    }
                }
            }
                return View(model);
            
        }
    }

}