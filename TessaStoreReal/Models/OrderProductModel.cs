using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TessaStoreReal.Models
{
    public class OrderProductModel
    {
        public Nullable<decimal> Price { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
    }

}