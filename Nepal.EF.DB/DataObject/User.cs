using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class User : IdentityUser
    {
        public string UserNo { get; set; }
        public string IPMANCode { get; set; }
        public string BusinessName { get; set; }
        public string RCNumber { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public bool isIPMAN { get; set; }
        public string CreditLimit { get; set; }
        public string CreditBalance { get; set; }
    }
}
