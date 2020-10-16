using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction
{
    public class Mailer
    {
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string CC { get; set; }
        public string DisplayName { get; set; }
        public bool DefaultCredentials { get; set; }
        public int Port { get; set; }
    }
}
