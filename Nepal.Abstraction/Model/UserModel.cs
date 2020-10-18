using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string IPMANCode { get; set; }
        public string BusinessName { get; set; }
        public string RCNumber { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserNo { get; set; }
        public bool isIPMAN { get; set; }
        public string CreditLimit { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CreditBalance { get; set; }
        public bool IsLockout { get; set; }
    }
}
