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
    public class ProgramService : IProgramService
    {
        private UserManager<User> _userManager;
        private OrderRepository _orderRepository;
        private ProgramRepository _programRepository;
        private IKYCClientService _kYCClientService;
        private readonly IMapper _mapper;
        public ProgramService(ProgramRepository programRepository, OrderRepository orderRepository,
            UserManager<User> userManager, IKYCClientService kYCClientService, IMapper mapper)
        {
            _programRepository = programRepository;
            _orderRepository = orderRepository;
            _kYCClientService = kYCClientService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task AddProgram(ProgramModel model)
        {
            var program = _mapper.Map<Program>(model);
            program.Status = 1;
            program.CreatedBy = "System";
            program.CreatedOn = DateTime.Now;
            
            var order = await _orderRepository.GetOrder(program.OrderId);
            var user = await _userManager.FindByIdAsync(order.UserId);
            var navOrder = _mapper.Map<NavProgramRequest>(program);
            navOrder.DocumentNo = order.OrderNo;
            navOrder.No = order.Product.Abbrev;
            var navTruck = await _kYCClientService.GetTruck(model.TruckNo);
            if (navTruck.Value.Length == 0)
            {
                var TruckReq = new NavTruckRequest
                {
                    RegNumber = model.TruckNo,
                    CustomerNo = user.UserNo
                };
                await _kYCClientService.PostTruck(TruckReq);
            }
            await _kYCClientService.PostProgram(navOrder);
            await _programRepository.Add(program);
        }

        public async Task DeleteProgram(int Id)
        {
            await _programRepository.Delete(Id);
        }

        public async Task<ProgramViewModel> GetProgram(int Id)
        {
            var program = await _programRepository.Get(Id);
            var order = await _orderRepository.Get(program.OrderId);
            program.Order = order;
            var navResponse = await _kYCClientService.GetProgram(order.OrderNo, program.TruckNo);
            var response = _mapper.Map<ProgramViewModel>(program);

            if (navResponse.Value.Length > 0)
            {
                var navProgram = navResponse.Value[0];
                response.Product = navProgram.No;
                response.QuantityInvoiced = navProgram.QuantityInvoiced;
                response.WaybillDate = (navProgram.WaybilDateTime > DateTime.MinValue) ? navProgram.WaybilDateTime.ToLongDateString() : null;
                response.QuantityShipped = navProgram.QuantityShipped;
                response.LoadingDate = (navProgram.TicketGenerationDateTime > DateTime.MinValue) ? navProgram.TicketGenerationDateTime.ToLongDateString() : null;
                response.LoadingTicketNo = navProgram.LoadingTicketNo;
                response.DispatchDate = (navProgram.DispatchedDateTime > DateTime.MinValue) ? navProgram.DispatchedDateTime.ToLongDateString() : null;
            }
            return response;
        }

        public async Task<List<ProgramViewModel>> GetPrograms(string userId)
        {
            var orders = await _programRepository.GetPrograms(userId);
            return _mapper.Map<List<ProgramViewModel>>(orders);
        }

        public async Task<List<ProgramViewModel>> GetPrograms(int OrderId)
        {
            var orders = await _programRepository.GetPrograms(OrderId);
            return _mapper.Map<List<ProgramViewModel>>(orders);
        }

        public async Task UpdateProgram(ProgramModel model, int Id)
        {
            var program = await _programRepository.Get(Id);
            program.OrderId = model.OrderId;
            program.Destination = model.Destination;
            program.Quantity = model.Quantity;
            program.TruckNo = model.TruckNo;
            program.Status = model.Status;
            program.ModifiedBy = "System";
            program.ModifiedOn = DateTime.Now;

            await _programRepository.Update(program);

        }

        public async Task<List<ProgramViewModel>> GetWorkingPrograms(string userId)
        {
            List<Program> result = new List<Program>();
            var programs = await _programRepository.GetPrograms(userId);
            var orderIds = programs.Select(c => c.Order.OrderNo).Distinct().ToList();
            var navResponse = await _kYCClientService.GetProgram(orderIds);
            if(navResponse.Value.Length > 0)
            {
                var wResponse = navResponse.Value.Where(c => !string.IsNullOrEmpty(c.LoadingTicketNo)).Select(c=>c.TruckNo).ToList();
                if(wResponse.Count > 0)
                result = programs.Where(p => wResponse.Contains(p.TruckNo.ToUpper())).ToList();
            }
            return _mapper.Map<List<ProgramViewModel>>(result);
        }
    }
}
