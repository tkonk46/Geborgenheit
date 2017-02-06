using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TessaStoreReal.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name ="Keep Me Logged In")]
        public bool PersistCookie { get; set; }
    }

}
