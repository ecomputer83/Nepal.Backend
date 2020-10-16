using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public partial class NavSale: BaseRequest
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("Document_Type")]
        public string DocumentType { get; set; }

        [JsonProperty("No")]
        public string No { get; set; }

        [JsonProperty("Sell_to_Customer_No")]
        public string SellToCustomerNo { get; set; }

        [JsonProperty("Sell_to_Customer_Name")]
        public string SellToCustomerName { get; set; }

        [JsonProperty("External_Document_No")]
        public string ExternalDocumentNo { get; set; }

        [JsonProperty("Sell_to_Post_Code")]
        public string SellToPostCode { get; set; }

        [JsonProperty("Sell_to_Country_Region_Code")]
        public string SellToCountryRegionCode { get; set; }

        [JsonProperty("Sell_to_Contact")]
        public string SellToContact { get; set; }

        [JsonProperty("Bill_to_Customer_No")]
        public string BillToCustomerNo { get; set; }

        [JsonProperty("Bill_to_Name")]
        public string BillToName { get; set; }

        [JsonProperty("Bill_to_Post_Code")]
        public string BillToPostCode { get; set; }

        [JsonProperty("Bill_to_Country_Region_Code")]
        public string BillToCountryRegionCode { get; set; }

        [JsonProperty("Bill_to_Contact")]
        public string BillToContact { get; set; }

        [JsonProperty("Ship_to_Code")]
        public string ShipToCode { get; set; }

        [JsonProperty("Ship_to_Name")]
        public string ShipToName { get; set; }

        [JsonProperty("Ship_to_Post_Code")]
        public string ShipToPostCode { get; set; }

        [JsonProperty("Ship_to_Country_Region_Code")]
        public string ShipToCountryRegionCode { get; set; }

        [JsonProperty("Ship_to_Contact")]
        public string ShipToContact { get; set; }

        [JsonProperty("Posting_Date")]
        public DateTimeOffset PostingDate { get; set; }

        [JsonProperty("Shortcut_Dimension_1_Code")]
        public string ShortcutDimension1_Code { get; set; }

        [JsonProperty("Shortcut_Dimension_2_Code")]
        public string ShortcutDimension2_Code { get; set; }

        [JsonProperty("Location_Code")]
        public string LocationCode { get; set; }

        [JsonProperty("Salesperson_Code")]
        public string SalespersonCode { get; set; }

        [JsonProperty("Assigned_User_ID")]
        public string AssignedUserId { get; set; }

        [JsonProperty("Currency_Code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("Document_Date")]
        public DateTimeOffset DocumentDate { get; set; }

        [JsonProperty("Requested_Delivery_Date")]
        public DateTimeOffset RequestedDeliveryDate { get; set; }

        [JsonProperty("Campaign_No")]
        public string CampaignNo { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Payment_Approval_Status")]
        public string PaymentApprovalStatus { get; set; }

        [JsonProperty("Product_Type")]
        public string ProductType { get; set; }

        [JsonProperty("Product_Unit_Price")]
        public long ProductUnitPrice { get; set; }

        [JsonProperty("Product_Quantity")]
        public long ProductQuantity { get; set; }

        [JsonProperty("Qty_Loaded")]
        public long QtyLoaded { get; set; }

        [JsonProperty("Qty_Remaining")]
        public long QtyRemaining { get; set; }

        [JsonProperty("Payment_Terms_Code")]
        public string PaymentTermsCode { get; set; }

        [JsonProperty("Due_Date")]
        public DateTimeOffset DueDate { get; set; }

        [JsonProperty("Payment_Discount_Percent")]
        public long PaymentDiscountPercent { get; set; }

        [JsonProperty("Shipment_Method_Code")]
        public string ShipmentMethodCode { get; set; }

        [JsonProperty("Shipping_Agent_Code")]
        public string ShippingAgentCode { get; set; }

        [JsonProperty("Shipping_Agent_Service_Code")]
        public string ShippingAgentServiceCode { get; set; }

        [JsonProperty("Package_Tracking_No")]
        public string PackageTrackingNo { get; set; }

        [JsonProperty("Shipment_Date")]
        public DateTimeOffset ShipmentDate { get; set; }

        [JsonProperty("Shipping_Advice")]
        public string ShippingAdvice { get; set; }

        [JsonProperty("Completely_Shipped")]
        public bool CompletelyShipped { get; set; }

        [JsonProperty("Job_Queue_Status")]
        public string JobQueueStatus { get; set; }

        [JsonProperty("Amt_Ship_Not_Inv_LCY_Base")]
        public long AmtShipNotInvLcyBase { get; set; }

        [JsonProperty("Amt_Ship_Not_Inv_LCY")]
        public long AmtShipNotInvLcy { get; set; }

        [JsonProperty("Amount")]
        public long Amount { get; set; }

        [JsonProperty("Amount_Including_VAT")]
        public long AmountIncludingVat { get; set; }

        [JsonProperty("Location_Filter")]
        public string LocationFilter { get; set; }
    }

    public partial class NavSaleResponse
    {
        public NavSale[] Value { get; set; }
    }
}
