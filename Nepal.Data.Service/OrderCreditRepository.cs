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
    public class OrderCreditRepository : CoreRepository<OrderCredit, CoreContext>
    {
        CoreContext _context;
        public OrderCreditRepository(CoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderCredit> GetByOrderId(int Id)
        {
            return await _context.OrderCredits.Include("Order").Include("Credit").FirstOrDefaultAsync(o => o.OrderId == Id);
        }

        public async Task<OrderCredit> GetByCreditId(int Id)
        {
            return await _context.OrderCredits.Include("Order").Include("Credit").FirstOrDefaultAsync(o => o.CreditId == Id);
        }
    }
}
