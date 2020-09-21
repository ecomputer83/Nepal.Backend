using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class Program: BaseObject, IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string TruckNo { get; set; }
        public string Destination { get; set; }
        public double Quantity { get; set; }
        public Order Order { get; set; }
    }
}
