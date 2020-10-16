using EASendMail;
using Limilabs.Client.SMTP;
using Limilabs.Mail;
using Limilabs.Mail.Fluent;
using Nepal.Abstraction;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service
{
    public class SmtpImplementation
    {
        public void SendMail(Mailer sender, string Body, string To, string From, string Subject)
        {
            try
            {
                IMail email = Mail
                        .Html(Body)
                            .To(To)
                            .From(From)
                            .Subject(Subject)
                        .Create();

                using (Smtp client = new Smtp())
                {
                    client.Connect(sender.Server);
                    client.StartTLS();

                    client.UseBestLogin(sender.UserName, sender.Password);
                    
                }
                
            }
            catch (Exception ep)
            {
                throw ep;
            }
        }
    }
}
