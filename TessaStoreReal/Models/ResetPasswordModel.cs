using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TessaStoreReal.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Password { get; set; }
        public string ResetToken { get; set; }
    }
}