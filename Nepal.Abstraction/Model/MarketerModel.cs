using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class MarketerModel
    {
        public string Code { get; set; }

        public UserModel User { get; set; }
        public ICollection<MarketerCustomerModel> MarketerCustomers { get; set; }
    }

    public class MarketerCustomerModel
    {
        public string CustomerName { get; set; }
        public string OrderQty { get; set; }
    }
}
