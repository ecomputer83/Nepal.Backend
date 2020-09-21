using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public partial class MailMessage : BaseObject
    {
        public MailMessage()
        {
            MailMessageAddress = new HashSet<MailMessageAddress>();
            MailMessageAttachment = new HashSet<MailMessageAttachment>();
        }

        public Guid MailMessageId { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string Priority { get; set; }
        public bool IsBodyHtml { get; set; }
        public bool IsSent { get; set; }
        public DateTimeOffset? MailSentDate { get; set; }
        public string Template { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string ErrorMessage { get; set; }

        public virtual ICollection<MailMessageAddress> MailMessageAddress { get; set; }
        public virtual ICollection<MailMessageAttachment> MailMessageAttachment { get; set; }
    }
}

