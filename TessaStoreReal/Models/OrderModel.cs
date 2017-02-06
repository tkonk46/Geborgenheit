using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TessaStoreReal.Models
{
    public class OrderModel
    {
        public Nullable<int> QuantityOrdered { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable <DateTime> DatePlaced { get; set; }
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public string ShippingStreet1 { get; set; }
        public string ShippingStreet2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string ShippingRecipient { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public int CreditCardNumber { get; set; }
        public int CreditCardVerificationValue { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardExpirationMonth { get; set; }
        public string CreditCardExpirationYear { get; set; }
    }
}