using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class Depot: IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public int Status { get; set; }
    }
}
