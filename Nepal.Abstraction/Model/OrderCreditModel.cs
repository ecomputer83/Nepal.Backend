using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class OrderCreditModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CreditId { get; set; }
        public OrderViewModel Order { get; set; }
        public CreditViewModel Credit { get; set; }
    }
}
