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
                .Include("Programs")
                .FirstOrDefaultAsync(c => c.Id == OrderId);

            return order;
        }

        public async Task<List<Order>> GetOrders(string UserId)
        {
            var order = await _context.Orders
                .Include("Product")
                .Include("Depot")
                .Include("Programs")
                .Where(c => c.UserId == UserId)
                .OrderByDescending(o=>o.OrderDate).ToListAsync();

            return order;
        }

        public async Task<int> AddOrder(Order order)
        {
            
           var _order = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return _order.Entity.Id;
        }
        
    }
}
