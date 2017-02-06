using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TessaStoreReal.Models
{
    public class ProductModel
    {
            public string Name { get; set; }
            public Nullable<int> Quantity { get; set; }
            public string Type { get; set; }
            public Nullable<decimal> Price { get; set; }
            public string ImageUrl { get; set; }
            public string Description { get; set; }
            public System.DateTime DateAdded { get; set; }
            public int ID { get; set; }
     }
}