using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class RegisterModel
    {
        public string IPMANCode { get; set; }
        public string BusinessName { get; set; }
        public string RCNumber { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public bool isIPMAN { get; set; }
        public string CreditLimit { get; set; }
        public string CreditBalance { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class DeviceInfoModel
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceModel { get; set; }
    }

    public class AdminModel
    {
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public IEnumerable<string> roleNames { get; set; }
    }

    public class RoleModel
    {
        public string Name { get; set; }
    }
}
