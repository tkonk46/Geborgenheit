using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TessaStoreReal.Models
{
    public class CheckoutModel
    {   
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Required]
        [CreditCard]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }
        [Required]
        [Display(Name = "CVV")]
        public string CreditCardVerificationValue { get; set; }
        [Required]
        [Display(Name = "Name On Card")]
        public string CreditCardName { get; set; }
        [Required]
        [Range(1,12)]
        [Display(Name = "Month")]
        public string CreditCardExpirationMonth { get; set; }
        [Required]
        [Range(2000,3000)]
        [Display(Name = "Year")]
        public string CreditCardExpirationYear { get; set; }

        [Required]
        [Display(Name ="Recipient")]
        public string ShippingRecipient { get; set; }
        [Required]
        [Display(Name ="Address Line 1")]
        public string ShippingStreet1 { get; set; }
        [Display(Name ="Address Line 2 (optional)")]
        public string ShippingStreet2 { get; set; }
        [Required]
        [Display(Name ="City")]
        public string ShippingCity { get; set; }
        [Required]
        [Display(Name ="State")]
        public string ShippingState { get; set; }
        [Required]
        [Display(Name ="Zip Code")]
        public string ShippingZip { get; set; }

        public OrderProductModel [] Products { get; set; }

    }
}