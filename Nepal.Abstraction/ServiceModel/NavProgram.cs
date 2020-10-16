using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public partial class NavProgram : BaseRequest
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; } = "";

        [JsonProperty("Document_Type")]
        public string DocumentType { get; set; } = "Order";

        [JsonProperty("Document_No")]
        public string DocumentNo { get; set; }

        [JsonProperty("No")]
        public string No { get; set; }

        [JsonProperty("Line_No")]
        public long LineNo { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; } = "";

        [JsonProperty("FilteredTypeField")]
        public string FilteredTypeField { get; set; } = "";

        [JsonProperty("Description")]
        public string Description { get; set; } = "";

        [JsonProperty("Location_Code")]
        public string LocationCode { get; set; } = "";

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("Unit_Price")]
        public long UnitPrice { get; set; }

        [JsonProperty("Unit_of_Measure_Code")]
        public string UnitOfMeasureCode { get; set; } = "";

        [JsonProperty("Unit_of_Measure")]
        public string UnitOfMeasure { get; set; } = "";

        [JsonProperty("Line_Amount")]
        public long LineAmount { get; set; }

        [JsonProperty("Qty_to_Ship")]
        public long QtyToShip { get; set; }

        [JsonProperty("Quantity_Shipped")]
        public long QuantityShipped { get; set; }

        [JsonProperty("Purchasing_Code")]
        public string PurchasingCode { get; set; } = "";

        [JsonProperty("Drop_Shipment")]
        public bool DropShipment { get; set; }

        [JsonProperty("Truck_No")]
        public string TruckNo { get; set; } = "";

        [JsonProperty("Destination")]
        public string Destination { get; set; } = "";

        [JsonProperty("Loading_Ticket_No")]
        public string LoadingTicketNo { get; set; } = "";

        [JsonProperty("Preferred_Name")]
        public string PreferredName { get; set; } = "";

        [JsonProperty("Qty_to_Invoice")]
        public long QtyToInvoice { get; set; }

        [JsonProperty("Quantity_Invoiced")]
        public long QuantityInvoiced { get; set; }
        [JsonProperty("TicketGenerationDateTime")]
        public DateTime TicketGenerationDateTime { get; set; }

        [JsonProperty("Dispatched")]
        public bool Dispatched { get; set; }

        [JsonProperty("Dispatched_DateTime")]
        public DateTime DispatchedDateTime { get; set; }

        [JsonProperty("Waybill_Created")]
        public bool WaybillCreated { get; set; }

        [JsonProperty("Waybil_DateTime")]
        public DateTime WaybilDateTime { get; set; }


        [JsonProperty("Shortcut_Dimension_1_Code")]
        public string ShortcutDimension1_Code { get; set; } = "";

        [JsonProperty("Shortcut_Dimension_2_Code")]
        public string ShortcutDimension2_Code { get; set; } = "";

        [JsonProperty("ShortcutDimCode_x005B_3_x005D_")]
        public string ShortcutDimCodeX005B3_X005D { get; set; } = "";

        [JsonProperty("ShortcutDimCode_x005B_4_x005D_")]
        public string ShortcutDimCodeX005B4_X005D { get; set; } = "";

        [JsonProperty("ShortcutDimCode_x005B_5_x005D_")]
        public string ShortcutDimCodeX005B5_X005D { get; set; } = "";

        [JsonProperty("ShortcutDimCode_x005B_6_x005D_")]
        public string ShortcutDimCodeX005B6_X005D { get; set; } = "";

        [JsonProperty("ShortcutDimCode_x005B_7_x005D_")]
        public string ShortcutDimCodeX005B7_X005D { get; set; } = "";

        [JsonProperty("ShortcutDimCode_x005B_8_x005D_")]
        public string ShortcutDimCodeX005B8_X005D { get; set; } = "";

        [JsonProperty("TotalSalesLine_Line_Amount")]
        public long TotalSalesLineLineAmount { get; set; }

        [JsonProperty("Invoice_Discount_Amount")]
        public long InvoiceDiscountAmount { get; set; }

        [JsonProperty("Invoice_Disc_Pct")]
        public long InvoiceDiscPct { get; set; }

        [JsonProperty("Total_Amount_Excl_VAT")]
        public long TotalAmountExclVat { get; set; }

        [JsonProperty("Total_VAT_Amount")]
        public long TotalVatAmount { get; set; }

        [JsonProperty("Total_Amount_Incl_VAT")]
        public long TotalAmountInclVat { get; set; }
    }

    public partial class NavProgramResponse
    {
        public NavProgram[] Value { get; set; }
    }

    public partial class NavProgramRequest : BaseRequest
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; } = "";

        [JsonProperty("Document_Type")]
        public string DocumentType { get; set; } = "Order";

        [JsonProperty("Document_No")]
        public string DocumentNo { get; set; }

        [JsonProperty("No")]
        public string No { get; set; }

        [JsonProperty("FilteredTypeField")]
        public string FilteredTypeField { get; set; } = "Item";

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("Qty_to_Ship")]
        public long QtyToShip { get; set; }

        [JsonProperty("Truck_No")]
        public string TruckNo { get; set; }

        [JsonProperty("Destination")]
        public string Destination { get; set; }
    }
}
