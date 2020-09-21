using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public partial class MailMessageAttachment
    {
        public Guid MailMessageAttachmentId { get; set; }
        public Guid MailMessageId { get; set; }
        public Guid DocumentId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public virtual MailMessage MailMessage { get; set; }
    }
}
