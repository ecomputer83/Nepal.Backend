
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class Order : BaseObject, IEntity
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Depot")]
        public int DepotId { get; set; }
        public string OrderNo { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
        public Depot Depot { get; set; }

        public ICollection<Program> Programs { get; set; }
    }
}
