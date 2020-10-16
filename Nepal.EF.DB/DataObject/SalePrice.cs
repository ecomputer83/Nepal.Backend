using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class SalesPrice : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Depot")]
        public int DepotId { get; set; }
        [ForeignKey("Product")]
        public int Productid { get; set; }
        public double Price { get; set; }

        public Depot Depot { get; set; }
        public Product Product { get; set; }
    }
}
