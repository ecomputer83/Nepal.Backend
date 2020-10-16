using Microsoft.EntityFrameworkCore;
using Nepal.EF.DB.Context;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Data.Service
{
    public class ProductRepository : CoreRepository<Product, CoreContext>
    {
        CoreContext _context;
        public ProductRepository(CoreContext context) : base(context)
        {
            _context = context;
        }

        public Task<Product> GetProductByAbbrev(string Abbrev)
        {
            return _context.Products.FirstOrDefaultAsync(c => c.Abbrev == Abbrev);
        }

    }
}
