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
    public class SalesPriceRepository : CoreRepository<SalesPrice, CoreContext>
    {
        private CoreContext _context;
        public SalesPriceRepository(CoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SalesPrice>> GetSalesPrice(int DepotId)
        {
            return await this._context.SalesPrices
                .Include("Product")
                .Include("Depot")
                .Where(c => c.DepotId == DepotId).ToListAsync();
        }

        public async Task<SalesPrice> GetSalesProductPrice(int ProductId)
        {
            return await this._context.SalesPrices
                .Include("Product")
                .Include("Depot")
                .FirstOrDefaultAsync(c => c.Productid == ProductId);
        }
    }
}
