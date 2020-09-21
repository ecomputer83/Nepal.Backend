using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface ICreditService
    {
        Task Create(CreditModel model);
        Task<CreditViewModel> GetCredit(int Id);
        Task<List<CreditViewModel>> GetCredits();
        Task UpdateCredit(CreditModel model, int Id);
        Task ApproveCredit(int CreditId);
        Task RejectCredit(int CreditId);
    }
}
