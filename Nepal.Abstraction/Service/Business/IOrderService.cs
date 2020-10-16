﻿using Nepal.Abstraction.Model;
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
        Task<List<OrderViewModel>> GetPendingOrders();
        Task<int> AddOrder(OrderModel model, string UserId);
        Task<List<OrderCreditModel>> GetCreditedOrders(string UserId);
        Task UpdateOrder(OrderModel model, int Id);
        Task CompleteOrder(int orderId);
        Task DeleteOrder(int Id);
    }
}
