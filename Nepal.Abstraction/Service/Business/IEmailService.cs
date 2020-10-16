using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IEmailService
    {
        Task SendOrderConfirmationAsync(OrderCreditModel oc, string Type, string To);
        Task SendOrderSummaryAsync(OrderViewModel oc, string To);
        Task SendAsync(string EmailDisplayName, string Subject, string Body, string From, string To);

        Task SendEmailConfirmationAsync(string Email, string Code);

        Task SendPasswordResetAsync(string Email, string Code);
    }
}
