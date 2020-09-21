using System;
using System.Collections.Generic;
using System.Text;

namespace BeStill.Abstraction.Model
{
    public class TokenModel
    {
        public bool? HasVerifiedEmail { get; set; }
        public bool? TFAEnabled { get; set; }
        public string Token { get; set; }
    }
}
