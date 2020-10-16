using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class Credit : BaseObject, IEntity
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Reference { get; set; }
        public DateTime CreditDate { get; set; }
    }
}
