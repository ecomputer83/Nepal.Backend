using Microsoft.Extensions.Options;
using Nepal.Abstraction;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Data.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly Mailer _email;
        private readonly MailTemplateRepository _mailTemplateRepository;
        public EmailService(IOptions<Mailer> email, MailTemplateRepository mailTemplateRepository)
        {
            _email = email.Value;
            _mailTemplateRepository = mailTemplateRepository;
        }

        public async Task SendOrderConfirmationAsync(OrderCreditModel oc, string Type, string To)
        {
            var template = await _mailTemplateRepository.GetByCode(Type);
            var body = template.TemplateBody;
            ConvertOrderCreditToBodyMessage(body, oc);
            using (var client = new SmtpClient(_email.Server, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }

                PrepareMailMessage(_email.DisplayName, template.TemplateSubject, body, _email.From, To, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }
        public async Task SendOrderSummaryAsync(OrderViewModel oc, string To)
        {
            var template = await _mailTemplateRepository.GetByCode("Order_Summary");
            var body = template.TemplateBody;
            ConvertOrderToBodyMessage(body, oc);
            using (var client = new SmtpClient(_email.Server, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }

                PrepareMailMessage(_email.DisplayName, template.TemplateSubject, body, _email.From, To, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }
        public async Task SendAsync(string EmailDisplayName, string Subject, string Body, string From, string To)
        {
            using (var client = new SmtpClient(_email.Server, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }

                PrepareMailMessage(EmailDisplayName, Subject, Body, From, To, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendEmailConfirmationAsync(string EmailAddress, string Code)
        {
            using (var client = new SmtpClient(_email.Server, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }

                PrepareMailMessage(_email.DisplayName, "Confirm your email", $"Here is the confirmation code <b>{Code}</b>", _email.From, EmailAddress, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendPasswordResetAsync(string EmailAddress, string Code)
        {
            using (var client = new SmtpClient(_email.Server, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }

                PrepareMailMessage(_email.DisplayName, "Reset your password", $"Please reset your password with code <b>{Code}</b>", _email.From, EmailAddress, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }

        private void PrepareMailMessage(string EmailDisplayName, string Subject, string Body, string From, string To, MailMessage mailMessage)
        {
            mailMessage.From = new MailAddress(From, EmailDisplayName);
            mailMessage.To.Add(To);
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Subject;
        }

        private void ConvertOrderCreditToBodyMessage(string body, OrderCreditModel model)
        {
            body.Replace("{PRODUCT}", model.Order.ProductName);
            body.Replace("{DEPOT}", model.Order.DepotName);
            body.Replace("{OrderNo}", model.Order.OrderNo);
            body.Replace("{Quantity}", model.Order.Quantity.ToString());
            body.Replace("{TotalAmount}", model.Order.TotalAmount.ToString());
            body.Replace("{Price}", (model.Order.TotalAmount / model.Order.Quantity).ToString());
            body.Replace("{Reference}", model.Credit.Reference.ToString());
            body.Replace("{Amount}", model.Credit.TotalAmount.ToString());
            body.Replace("{Date}", model.Credit.CreditDate.ToShortDateString());
        }

        private void ConvertOrderToBodyMessage(string body, OrderViewModel model)
        {
            body.Replace("{PRODUCT}", model.ProductName);
            body.Replace("{DEPOT}", model.DepotName);
            body.Replace("{OrderNo}", model.OrderNo);
            body.Replace("{Quantity}", model.Quantity.ToString());
            body.Replace("{TotalAmount}", model.TotalAmount.ToString());
            body.Replace("{Price}", (model.TotalAmount / model.Quantity).ToString());
        }
    }
}
