using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class OrderCredit : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Credit")]
        public int CreditId { get; set; }
        public Order Order { get; set; }
        public Credit Credit { get; set; }
        
    }
}
