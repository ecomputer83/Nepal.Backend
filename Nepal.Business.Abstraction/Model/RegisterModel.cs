using BeStill.Abstraction.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeStill.Abstraction.Model
{
    public class RegisterModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DeviceInfoModel DeviceInfo { get; set; }
    }

    public class DeviceInfoModel
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceModel { get; set; }
    }
}
