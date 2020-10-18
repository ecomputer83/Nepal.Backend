using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Abstraction.ServiceModel;
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
        private ProductRepository _productRepository;
        private DepotRepository _depotRepository;
        private UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private IKYCClientService _kYCClientService;
        private readonly IMapper _mapper;
        public OrderService(OrderRepository orderRepository, ProgramRepository programRepository,
            CreditRepository creditRepository, OrderCreditRepository orderCreditRepository,
            ProductRepository productRepository, DepotRepository depotRepository, 
            UserManager<User> userManager, IKYCClientService kYCClientService, IMapper mapper, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _programRepository = programRepository;
            _creditRepository = creditRepository;
            _orderCreditRepository = orderCreditRepository;
            _productRepository = productRepository;
            _depotRepository = depotRepository;
            _kYCClientService = kYCClientService;
            _userManager = userManager;
            _emailService = emailService;
            _mapper = mapper;
        }
        public async Task<int> AddOrder(OrderModel model, string UserId)
        {
            var order = _mapper.Map<Order>(model);
            //order.OrderNo = "P" + GenerateRandomNo();
            order.UserId = UserId;
            order.OrderDate = DateTime.Now;
            order.CreatedBy = "System";
            order.CreatedOn = DateTime.Now;
            var user = await _userManager.FindByIdAsync(UserId);
            var prod = await _productRepository.Get(order.ProductId);
            var depot = await _depotRepository.Get(order.DepotId);
            //var navOrder = _mapper.Map<NavSaleRequest>(order);
            //navOrder.DocumentType = "Order";
            //navOrder.ProductType = prod.Abbrev;
            //navOrder.LocationCode = depot.Code;
            //navOrder.SellToCustomerName = user.BusinessName;
            //navOrder.SellToCustomerNo = user.UserNo;
            //navOrder.DocumentDate = navOrder.DueDate = navOrder.OrderDate = navOrder.PostingDate  = order.OrderDate.ToString("yyyy-MM-dd");
            //var _order = await _kYCClientService.PostOrder(navOrder);
            //order.OrderNo = _order.No.ToString();
            var id = await _orderRepository.AddOrder(order);
            order.Product = prod;
            order.Depot = depot;
            await _emailService.SendOrderSummaryAsync(_mapper.Map<OrderViewModel>(order), user.Email);
            return id;
        }

        public async Task PostOrder(string UserId)
        {
            var orders = await _orderRepository.GetOrders(UserId);
            if(orders.Count > 0)
            {
                var order = orders[0];
                var user = await _userManager.FindByIdAsync(UserId);
                var prod = await _productRepository.Get(order.ProductId);
                var depot = await _depotRepository.Get(order.DepotId);
                var navOrder = _mapper.Map<NavSaleRequest>(order);
                navOrder.DocumentType = "Order";
                navOrder.ProductType = prod.Abbrev;
                navOrder.LocationCode = depot.Code;
                navOrder.SellToCustomerName = user.BusinessName;
                navOrder.SellToCustomerNo = user.UserNo;
                navOrder.DocumentDate = navOrder.DueDate = navOrder.OrderDate = navOrder.PostingDate = order.OrderDate.ToString("yyyy-MM-dd");
                var _order = await _kYCClientService.PostOrder(navOrder);
                order.OrderNo = _order.No.ToString();

                await _orderRepository.Update(order);
            }
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

        public async Task<List<OrderCreditModel>> GetCreditedOrders(string UserId)
        {
            var orders = await _creditRepository.GetCredits(UserId);
            return _mapper.Map<List<OrderCreditModel>>(orders);
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var orders = await _orderRepository.GetOrders();
            return _mapper.Map<List<OrderViewModel>>(orders);
        }

        public async Task<List<OrderViewModel>> GetPendingOrders()
        {
            var orders = await _orderRepository.GetPendingOrders();
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
