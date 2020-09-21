using Microsoft.EntityFrameworkCore;
using Nepal.EF.DB.Context;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Data.Service
{
    public class CreditRepository : CoreRepository<Credit, CoreContext>
    {
        CoreContext _context;
        public CreditRepository(CoreContext context) : base(context)
        {
            _context = context;
        }
        public async Task Create(Credit model, int Id)
        {
            var credit = await this.Add(model);
            _context.OrderCredits.Add(new OrderCredit { CreditId = credit.Id, OrderId = Id });
            await _context.SaveChangesAsync();
        }

        public async Task ApproveCredit(int creditId)
        {
            var credit = await this.Get(creditId);
            credit.Status = 1;
            credit = await this.Update(credit);
            if(credit.Status == 1)
            {
                var orderCredit = await _context.OrderCredits.Include("Order").FirstOrDefaultAsync(o => o.CreditId == creditId);
                var order = orderCredit.Order;
                order.Status = 1;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }

        }

        public async Task RejectCredit(int creditId)
        {
            var credit = await this.Get(creditId);
            credit.Status = 8;
            credit = await this.Update(credit);
            if (credit.Status == 8)
            {
                var orderCredit = await _context.OrderCredits.Include("Order").FirstOrDefaultAsync(o => o.CreditId == creditId);
                var order = orderCredit.Order;
                order.Status = 8;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }

        }
    }
}
