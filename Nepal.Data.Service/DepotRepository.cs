using Microsoft.EntityFrameworkCore;
using Nepal.EF.DB.Context;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Data.Service
{
    public class DepotRepository : CoreRepository<Depot, CoreContext>
    {
        CoreContext _context;
        public DepotRepository(CoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Depot> GetDepotByCode(string code)
        {
            return await _context.Depots.FirstOrDefaultAsync(c => c.Code == code);
        }
    }
}
