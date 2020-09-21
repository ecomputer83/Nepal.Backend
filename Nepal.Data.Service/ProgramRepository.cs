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
    public class ProgramRepository : CoreRepository<Program, CoreContext>
    {
        CoreContext _context;
        public ProgramRepository(CoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeletePrograms(int OrderId)
        {
            var ProgramIds = _context.Programs
                .Where(p => p.OrderId == OrderId)
                .Select(c => c.Id).ToList();
            foreach(var Id in ProgramIds)
            {
                await this.Delete(Id);
            }
        }

        public async Task<List<Program>> GetPrograms(int orderId)
        {
            return await _context.Programs.Where(p => p.OrderId == orderId).ToListAsync();
        }
        public async Task<List<Program>> GetPrograms(string userId)
        {
            return await _context.Programs.Include("Order").Where(p => p.Order.UserId == userId).ToListAsync();
        }
    }
}
