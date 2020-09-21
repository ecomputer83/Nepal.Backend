using Nepal.EF.DB.Context;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Data.Service
{
    public class ProductRepository : CoreRepository<Product, CoreContext>
    {
        public ProductRepository(CoreContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
