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
    public class MarketerRepository : CoreRepository<Marketer, CoreContext>
    {
        CoreContext _context;
        public MarketerRepository(CoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Marketer> GetMarketer(string UserId)
        {
            var Marketer = _context.Marketers.FirstOrDefault(c => c.UserId == UserId);
            if(Marketer != null)
            Marketer.MarketerCustomers = await _context.MarketerCustomers.Where(c => c.MarketerId == Marketer.Id).ToListAsync();

            return Marketer;
        }
    }
}
