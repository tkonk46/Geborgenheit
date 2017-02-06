using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TessaStoreReal.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public OrderProductModel[] Products { get; set; }
    }
}