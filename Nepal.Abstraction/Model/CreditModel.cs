using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class CreditModel
    {
        public int OrderId { get; set; }
        public double TotalAmount { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Reference { get; set; }
        public DateTime CreditDate { get; set; }
    }

    public class CreditViewModel
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string CreditType { get { return (Type == 1) ? "Card Payment" : (Type == 2) ? "IPMAN Credit" : (Type == 3) ? "Bank Deposit" : "Credit"; } }
        public string Reference { get; set; }
        public DateTime CreditDate { get; set; }
    }
}
