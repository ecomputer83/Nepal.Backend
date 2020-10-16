using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Abstraction.ServiceModel;
using Nepal.Data.Service;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service
{
    public class CreditService : ICreditService
    {
        private CreditRepository _creditRepository;
        private OrderCreditRepository _orderCreditRepository;
        private UserManager<User> _userService;
        private IKYCClientService _kYCClientService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreditService(CreditRepository creditRepository, 
            OrderCreditRepository orderCreditRepository, UserManager<User> userService,
            IKYCClientService kYCClientService, IMapper mapper, IEmailService emailService)
        {
            _creditRepository = creditRepository;
            _orderCreditRepository = orderCreditRepository;
            _kYCClientService = kYCClientService;
            _userService = userService;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<int> Create(CreditModel model)
        {
            var credit = _mapper.Map<Credit>(model);
            credit.Status = 0;
            credit.CreatedBy = "System";
            credit.CreatedOn = DateTime.Now;
            var id = await _creditRepository.Create(credit, model.OrderId);
            var oCredit = await _orderCreditRepository.GetByOrderId(model.OrderId);
            var navOrder = await _kYCClientService.GetSaleDetail(oCredit.Order.OrderNo);
            if (navOrder.Value.Length > 0)
            {
                var order = _mapper.Map<NavSaleRequest>(navOrder.Value[0]);
                order.Bank = model.Name;
                order.ProductAmount = Convert.ToInt64(oCredit.Credit.TotalAmount);
                order.AmountPaid = Convert.ToInt64(oCredit.Credit.TotalAmount);
                await _kYCClientService.PutOrder(navOrder.Value[0].No, order);
            }
            var user = await _userService.FindByIdAsync(oCredit.Order.UserId);
            if (model.Type == 2)
            {
                
                var creditBalance = long.Parse(user.CreditBalance) - model.TotalAmount;
                user.CreditBalance = creditBalance.ToString();
                await _userService.UpdateAsync(user);
                await _emailService.SendOrderConfirmationAsync(_mapper.Map<OrderCreditModel>(oCredit), "Order_Request_Credit", user.Email);
            }
            else
            {
                await _emailService.SendOrderConfirmationAsync(_mapper.Map<OrderCreditModel>(oCredit), "Order_Bank_Notification", user.Email);
            }
            return id;
        }

        public async Task<CreditViewModel> GetCredit(int Id)
        {
            var credit = await _creditRepository.Get(Id);
            return _mapper.Map<CreditViewModel>(credit);
        }

        public async Task<List<CreditViewModel>> GetCredits()
        {
            var credits = await _creditRepository.GetAll();
            return _mapper.Map<List<CreditViewModel>>(credits);
        }
        public async Task<List<OrderCreditModel>> GetBankDeposits()
        {
            var credits = await _creditRepository.GetBankDeposits();
            return _mapper.Map<List<OrderCreditModel>>(credits);
        }

        public async Task<List<OrderCreditModel>> GetIPMANCredits()
        {
            var credits = await _creditRepository.GetIPMANCredits();
            return _mapper.Map<List<OrderCreditModel>>(credits);
        }

        public async Task UpdateCredit(CreditModel model, int Id)
        {
            var credit = await _creditRepository.Get(Id);
            credit.ModifiedBy = "System";
            credit.ModifiedOn = DateTime.Now;
            credit.Reference = model.Reference;
            credit.TotalAmount = model.TotalAmount;
            credit.Type = model.Type;

            await _creditRepository.Update(credit);
        }

        public async Task ApproveCredit(int CreditId)
        {
            await _creditRepository.ApproveCredit(CreditId);

            var oCredit = await _orderCreditRepository.GetByCreditId(CreditId);
            var navOrder = await _kYCClientService.GetSaleDetail(oCredit.Order.OrderNo);
            var user = await _userService.FindByIdAsync(oCredit.Order.UserId);
            if (navOrder.Value.Length > 0)
            {
                var order = _mapper.Map<NavSaleRequest>(navOrder.Value[0]);
                //order.PaymentApprovalStatus = "Approved";

                await _kYCClientService.PutOrder(navOrder.Value[0].No, order);
                if(oCredit.Credit.Type == 3)
                {
                    await _emailService.SendOrderConfirmationAsync(_mapper.Map<OrderCreditModel>(oCredit), "Order_Bank_Approval", user.Email);
                }
                else if(oCredit.Credit.Type == 2)
                {
                    await _emailService.SendOrderConfirmationAsync(_mapper.Map<OrderCreditModel>(oCredit), "Order_Credit_Approve", user.Email);
                }
                else
                {
                    await _emailService.SendOrderConfirmationAsync(_mapper.Map<OrderCreditModel>(oCredit), "Order_Card_Approve", user.Email);
                }
            }
        }

        public async Task RejectCredit(int CreditId)
        {
            await _creditRepository.RejectCredit(CreditId);

            var oCredit = await _orderCreditRepository.GetByCreditId(CreditId);
            var navOrder = await _kYCClientService.GetSaleDetail(oCredit.Order.OrderNo);
            var user = await _userService.FindByIdAsync(oCredit.Order.UserId);
            if (oCredit.Credit.Type == 2)
            {
                
                var creditBalance = long.Parse(user.CreditBalance) + oCredit.Credit.TotalAmount;
                user.CreditBalance = creditBalance.ToString();
                await _userService.UpdateAsync(user);
                await _emailService.SendOrderConfirmationAsync(_mapper.Map<OrderCreditModel>(oCredit), "Order_Credit_Approve", user.Email);
            }
            else if (oCredit.Credit.Type == 3)
            {
                await _emailService.SendOrderConfirmationAsync(_mapper.Map<OrderCreditModel>(oCredit), "Order_Bank_Approval", user.Email);
            }

            if (navOrder.Value.Length > 0)
            {
                var order = _mapper.Map<NavSaleRequest>(navOrder.Value[0]);
                //order.PaymentApprovalStatus = "Rejected";

                await _kYCClientService.PutOrder(navOrder.Value[0].No, order);
            }
        }
    }
}
