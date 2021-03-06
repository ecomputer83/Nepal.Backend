﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class OrderModel
    {
        public int ProductId { get; set; }
        public int DepotId { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
    }

    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string ProductName { get; set; }
        public string DepotName { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ProgramViewModel> Programs { get; set; }
        public CreditViewModel Credit { get; set; }
        public UserModel User { get; set; }
        public int Status { get; set; }
    }
}
