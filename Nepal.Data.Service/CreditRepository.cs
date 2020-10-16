using Microsoft.EntityFrameworkCore;
using Nepal.EF.DB.Context;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task<int> Create(Credit model, int Id)
        {
            var credit = await this.Add(model);
            _context.OrderCredits.Add(new OrderCredit { CreditId = credit.Id, OrderId = Id });
            await _context.SaveChangesAsync();
            return credit.Id;
        }
        public async Task<List<OrderCredit>> GetBankDeposits()
        {
            return await (from oc in _context.OrderCredits
                          join op in (from o in _context.Orders
                                      join u in _context.Users on o.UserId equals u.Id
                                      join p in _context.Products on o.ProductId equals p.Id
                                      join d in _context.Depots on o.DepotId equals d.Id
                                      select new Order
                                      {
                                          Id = o.Id,
                                          OrderDate = o.OrderDate,
                                          OrderNo = o.OrderNo,
                                          Status = o.Status,
                                          Depot = d,
                                          Product = p,
                                          User = u
                                      })
                                          on oc.OrderId equals op.Id
                          join c in _context.Credits on oc.CreditId equals c.Id
                          where c.Status == 0 && c.Type == 3
                          orderby c.CreatedOn
                          select new OrderCredit
                          {
                              Id = oc.Id,
                              CreditId = oc.CreditId,
                              OrderId = oc.OrderId,
                              Credit = c,
                              Order = op
                          }).ToListAsync();
        }
        public async Task<List<OrderCredit>> GetCredits(string userId)
        {
            return await (from oc in _context.OrderCredits
                          join op in (from o in _context.Orders
                                      join u in _context.Users on o.UserId equals u.Id
                                      join p in _context.Products on o.ProductId equals p.Id
                                      join d in _context.Depots on o.DepotId equals d.Id
                                      where u.Id == userId
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
                                          User = u
                                      })
                                          on oc.OrderId equals op.Id
                          join c in _context.Credits on oc.CreditId equals c.Id
                          
                          orderby c.CreatedOn
                          select new OrderCredit
                          {
                              Id = oc.Id,
                              CreditId = oc.CreditId,
                              OrderId = oc.OrderId,
                              Credit = c,
                              Order = op
                          }).ToListAsync();
        }
        public async Task<List<OrderCredit>> GetIPMANCredits()
        {
            return await (from oc in _context.OrderCredits
                          join op in (from o in _context.Orders
                                      join u in _context.Users on o.UserId equals u.Id
                                      join p in _context.Products on o.ProductId equals p.Id
                                      join d in _context.Depots on o.DepotId equals d.Id
                                      select new Order
                                      {
                                          Id = o.Id,
                                          OrderDate = o.OrderDate,
                                          OrderNo = o.OrderNo,
                                          Status = o.Status,
                                          Depot = d,
                                          Product = p,
                                          User = u
                                      })
                                          on oc.OrderId equals op.Id
                          join c in _context.Credits on oc.CreditId equals c.Id
                          where c.Status == 0 && c.Type == 2
                          orderby c.CreatedOn
                          select new OrderCredit
                          {
                              Id = oc.Id,
                              CreditId = oc.CreditId,
                              OrderId = oc.OrderId,
                              Credit = c,
                              Order = op
                          }).ToListAsync();
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
