using AutoMapper;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Data.Service;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service
{
    public class OrderService : IOrderService
    {
        private OrderRepository _orderRepository;
        private ProgramRepository _programRepository;
        private OrderCreditRepository _orderCreditRepository;
        private CreditRepository _creditRepository;
        private readonly IMapper _mapper;
        public OrderService(OrderRepository orderRepository, ProgramRepository programRepository,
            CreditRepository creditRepository, OrderCreditRepository orderCreditRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _programRepository = programRepository;
            _creditRepository = creditRepository;
            _orderCreditRepository = orderCreditRepository;
            _mapper = mapper;
        }
        public async Task<int> AddOrder(OrderModel model, string UserId)
        {
            var order = _mapper.Map<Order>(model);
            order.OrderNo = "P" + GenerateRandomNo();
            order.UserId = UserId;
            order.OrderDate = DateTime.Now;
            order.CreatedBy = "System";
            order.CreatedOn = DateTime.Now;
            return await _orderRepository.AddOrder(order);
        }

        public async Task DeleteOrder(int Id)
        {
            var order = await _orderRepository.Get(Id);
           
            
            var oCredit = await _orderCreditRepository.GetByOrderId(Id);
            if (oCredit != null)
            {
                await _orderCreditRepository.Delete(oCredit.Id);
                await _creditRepository.Delete(oCredit.CreditId);
                
            }
            await _orderRepository.Delete(Id);
            await _programRepository.DeletePrograms(Id);
        }

        public async Task<OrderViewModel> GetOrder(int Id)
        {
            var order = await _orderRepository.GetOrder(Id);
            var _order = _mapper.Map<OrderViewModel>(order);
            var oCredit = await _orderCreditRepository.GetByOrderId(Id);
            if (oCredit != null)
            {
                var _credit = oCredit.Credit;
                _order.Credit = _mapper.Map<CreditViewModel>(_credit);
            }

            return _order;
        }
        public async Task<List<OrderViewModel>> GetOrders(string UserId)
        {
            var orders = await _orderRepository.GetOrders(UserId);
            return _mapper.Map<List<OrderViewModel>>(orders);
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var orders = await _orderRepository.GetOrders();
            return _mapper.Map<List<OrderViewModel>>(orders);
        }

        public async Task UpdateOrder(OrderModel model, int Id)
        {
            var order = await _orderRepository.Get(Id);
            order.DepotId = model.DepotId;
            order.ProductId = model.ProductId;
            order.Quantity = model.Quantity;
            order.ModifiedBy = "System";
            order.ModifiedOn = DateTime.Now;
            await _orderRepository.Update(order);
        }
        private int GenerateRandomNo()
        {
            int _min = 10000;
            int _max = 99999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public async Task CompleteOrder(int orderId)
        {
            await _orderRepository.CompleteOrder(orderId);
        }
    }
}
