using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IOrderService
    {
        Task<OrderViewModel> GetOrder(int Id);
        Task<List<OrderViewModel>> GetOrders(string UserId);
        Task<List<OrderViewModel>> GetOrders();
        Task<int> AddOrder(OrderModel model, string UserId);
        Task UpdateOrder(OrderModel model, int Id);
        Task DeleteOrder(int Id);
    }
}
