using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class MarketerCustomer: IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Marketer")]
        public int MarketerId { get; set; }
        public string CustomerName { get; set; }
        public string OrderQty { get; set; }

        public int Status { get; set; }

        public Marketer Marketer { get; set; }
    }
}
