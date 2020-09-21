
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class Marketer : IEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string Code { get; set; }

        public User User { get; set; }
        public ICollection<MarketerCustomer> MarketerCustomers { get; set; }
    }
}
