using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class ProgramModel
    {
        public int OrderId { get; set; }
        public string TruckNo { get; set; }
        public string Destination { get; set; }
        public double Quantity { get; set; }
        public int Status { get; set; }
    }

    public class ProgramViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string Product { get; set; }
        public string TruckNo { get; set; }
        public string Destination { get; set; }
        public double Quantity { get; set; }
        public long QuantityInvoiced { get; set; }
        public string LoadingTicketNo { get; set; }
        public long QuantityShipped { get; set; }
        public string WaybillNo { get; set; }
        public string ProgramDate { get; set; }
        public string LoadingDate { get; set; }
        public string DispatchDate { get; set; }
        public string WaybillDate { get; set; }
    }
}
