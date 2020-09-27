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
    public class OrderRepository : CoreRepository<Order, CoreContext>
    {
        CoreContext _context;
        public OrderRepository(CoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder(int OrderId)
        {
            var order = await _context.Orders
                .Include("Product")
                .Include("Depot")
                .FirstOrDefaultAsync(c => c.Id == OrderId);

            order.Programs = await _context.Programs.Where(p => p.OrderId == OrderId).ToListAsync();
            return order;
        }

        public async Task<List<Order>> GetOrders(string UserId)
        {
            var order = await _context.Orders
                .Include("Product")
                .Include("Depot")
                //.Include("Programs")
                .Where(c => c.UserId == UserId && c.Status != 2)
                .OrderByDescending(o=>o.OrderDate).ToListAsync();

            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            var order = await _context.Orders
                .Include("Product")
                .Include("Depot")
                //.Include("Programs")
                .Where(c => c.Status == 1)
                .OrderByDescending(o => o.OrderDate).ToListAsync();

            return order;
        }

        public async Task<int> AddOrder(Order order)
        {
            
           var _order = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return _order.Entity.Id;
        }

        public async Task CompleteOrder(int OrderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
            order.Status = 2;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

    }
}
