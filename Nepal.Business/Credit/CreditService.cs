using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
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
        private readonly IMapper _mapper;
        public CreditService(CreditRepository creditRepository, IMapper mapper)
        {
            _creditRepository = creditRepository;
            _mapper = mapper;
        }
        public async Task<int> Create(CreditModel model)
        {
            var credit = _mapper.Map<Credit>(model);
            credit.Status = 1;
            credit.CreatedBy = "System";
            credit.CreatedOn = DateTime.Now;
            return await _creditRepository.Create(credit, model.OrderId);
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
        }

        public async Task RejectCredit(int CreditId)
        {
            await _creditRepository.RejectCredit(CreditId);
        }
    }
}
