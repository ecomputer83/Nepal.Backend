using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class ProductModel : GenericModel
    {
        public string Product { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
    }
}
