using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nepal.Abstraction;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Data.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly Mailer _email;
        private readonly IConfiguration _configuration;
        private readonly MailTemplateRepository _mailTemplateRepository;
        public EmailService(IOptions<Mailer> email, IConfiguration configuration, MailTemplateRepository mailTemplateRepository)
        {
            _email = configuration.GetSection("Email").Get<Mailer>(); 
            _configuration = configuration;
            _mailTemplateRepository = mailTemplateRepository;
        }

        public async Task SendOrderConfirmationAsync(OrderCreditModel oc, string Type, string To)
        {
            var template = await _mailTemplateRepository.GetByCode(Type);
            var body = template.TemplateBody;
            body = ConvertOrderCreditToBodyMessage(body, oc);
            using (var client = new SmtpClient(_email.Server, _email.Port))
            {
                using (var mailMessage = new MailMessage())
                {
                    if (!_email.DefaultCredentials)
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                    }
                    var cc = _email.CC;
                    PrepareMailMessage(_email.DisplayName, template.TemplateSubject, body, _email.From, To, mailMessage, cc);
                    client.EnableSsl = true;
                    await client.SendMailAsync(mailMessage);
                }
            }
        }
        public async Task SendOrderSummaryAsync(OrderViewModel oc, string To)
        {
            var template = await _mailTemplateRepository.GetByCode("Order_Summary");
            var body = template.TemplateBody;
            body = ConvertOrderToBodyMessage(body, oc);
            using (var client = new SmtpClient(_email.Server, _email.Port))
            {
                using (var mailMessage = new MailMessage())
                {
                    if (!_email.DefaultCredentials)
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                    }
                    var cc = _email.CC;
                    PrepareMailMessage(_email.DisplayName, template.TemplateSubject, body, _email.From, To, mailMessage, cc);
                    client.EnableSsl = true;
                    await client.SendMailAsync(mailMessage);
                }
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
                client.EnableSsl = true;
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
                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendPasswordResetAsync(string EmailAddress, string Code)
        {
            using (var client = new SmtpClient(_email.Server, _email.Port))
            {
                using (var mailMessage = new MailMessage())
                {
                    if (!_email.DefaultCredentials)
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                    }

                    PrepareMailMessage(_email.DisplayName, "Reset your password", $"Please reset your password with code <b>{Code}</b>", _email.From, EmailAddress, mailMessage);
                    client.EnableSsl = true;
                    await client.SendMailAsync(mailMessage);
                }
            }
        }

        private void PrepareMailMessage(string EmailDisplayName, string Subject, string Body, string From, string To, MailMessage mailMessage, string cc = null)
        {
            mailMessage.From = new MailAddress(From, EmailDisplayName);
            mailMessage.To.Add(To);
            if(!string.IsNullOrEmpty(cc))
            mailMessage.Bcc.Add(cc);

            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Subject;
        }

        private string ConvertOrderCreditToBodyMessage(string xc, OrderCreditModel model)
        {
            var body = xc;
            body = body.Replace("{PRODUCT}", model.Order.ProductName);
            body = body.Replace("{DEPOT}", model.Order.DepotName);
            body = body.Replace("{OrderNo}", model.Order.OrderNo);
            body = body.Replace("{Quantity}", model.Order.Quantity.ToString("N", CultureInfo.CurrentCulture));
            body = body.Replace("{TotalAmount}", "₦ " + model.Order.TotalAmount.ToString("N", CultureInfo.CurrentCulture));
            body = body.Replace("{Price}", "₦ " + (model.Order.TotalAmount / model.Order.Quantity).ToString("N", CultureInfo.CurrentCulture));
            body = body.Replace("{Account}", (model.Credit.Type == 3) ? "Bank Deposit" : (model.Credit.Type == 2) ? "IPMAN Credit" : "Card Payment");
            body = body.Replace("{Reference}", model.Credit.Reference.ToString());
            body = body.Replace("{Amount}", "₦ " + model.Credit.TotalAmount.ToString("N", CultureInfo.CurrentCulture));
            body = body.Replace("{Date}", model.Credit.CreditDate.ToShortDateString());

            return body;
        }

        private string ConvertOrderToBodyMessage(string xc, OrderViewModel model)
        {
            var body = xc;
            body = body.Replace("{PRODUCT}", model.ProductName);
            body = body.Replace("{DEPOT}", model.DepotName);
            body = body.Replace("{OrderNo}", model.OrderNo);
            body = body.Replace("{Quantity}", model.Quantity.ToString("N", CultureInfo.CurrentCulture));
            body = body.Replace("{TotalAmount}", "₦ " + model.TotalAmount.ToString("N", CultureInfo.CurrentCulture));
            body = body.Replace("{Price}", "₦ " + (model.TotalAmount / model.Quantity).ToString("N", CultureInfo.CurrentCulture));

            return body;
        }
    }
}
