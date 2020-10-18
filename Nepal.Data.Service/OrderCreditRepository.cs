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
            return await (from oc in _context.OrderCredits
                          join op in (from o in _context.Orders
                                      join u in _context.Users on o.UserId equals u.Id
                                      join p in _context.Products on o.ProductId equals p.Id
                                      join d in _context.Depots on o.DepotId equals d.Id
                                      where o.Id == Id
                                      select new Order
                                      {
                                          Id = o.Id,
                                          Programs = (from p in _context.Programs where p.OrderId == o.Id select p).ToList(),
                                          OrderDate = o.OrderDate,
                                          OrderNo = o.OrderNo,
                                          Quantity = o.Quantity,
                                          TotalAmount = o.TotalAmount,
                                          Status = o.Status,
                                          Depot = d,
                                          Product = p,
                                          User = u,
                                          UserId = u.Id
                                      })
                                          on oc.OrderId equals op.Id
                          join c in _context.Credits on oc.CreditId equals c.Id
                          select new OrderCredit
                          {
                              Id = oc.Id,
                              CreditId = oc.CreditId,
                              OrderId = oc.OrderId,
                              Credit = c,
                              Order = op
                          }).FirstOrDefaultAsync();
        }

        public async Task<OrderCredit> GetByCreditId(int Id)
        {
            return await _context.OrderCredits.Include("Order").Include("Credit").FirstOrDefaultAsync(o => o.CreditId == Id);
        }
    }
}
