using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class MailTemplate : BaseObject
    {
        public int Id { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateBody { get; set; }
        public string Priority { get; set; }
        public int IsBodyHtml { get; set; }
        public int CopySender { get; set; }
        public bool? IsSystem { get; set; }
    }
}
