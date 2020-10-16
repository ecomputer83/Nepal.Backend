using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public partial class NavSaleDetail: BaseRequest
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; } = "";

        [JsonProperty("Document_Type")]
        public string DocumentType { get; set; } = "";

        [JsonProperty("No")]
        public string No { get; set; } = "";

        [JsonProperty("Sell_to_Customer_No")]
        public string SellToCustomerNo { get; set; } = "";

        [JsonProperty("Sell_to_Customer_Name")]
        public string SellToCustomerName { get; set; } = "";

        [JsonProperty("Location_Code")]
        public string LocationCode { get; set; } = "";

        [JsonProperty("Product_Type")]
        public string ProductType { get; set; } = "";

        [JsonProperty("Product_Amount")]
        public long ProductAmount { get; set; }

        [JsonProperty("External_Document_No")]
        public string ExternalDocumentNo { get; set; } = "";

        [JsonProperty("Bank")]
        public string Bank { get; set; } = "";

        [JsonProperty("Bank_Name")]
        public string BankName { get; set; } = "";

        [JsonProperty("Amount_Paid")]
        public long AmountPaid { get; set; }

        [JsonProperty("Payment_Approval_Status")]
        public string PaymentApprovalStatus { get; set; } = "";

        [JsonProperty("Bill_to_Name")]
        public string BillToName { get; set; } = "";



    }

    public partial class NavSaleDetailResponse
    {
        public NavSaleDetail[] Value { get; set; }
    }

    public class NavSaleRequest: BaseRequest
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; } = "";

        [JsonProperty("Document_Type")]
        public string DocumentType { get; set; } = "Order";


        [JsonProperty("Sell_to_Customer_No")]
        public string SellToCustomerNo { get; set; } = "";

        [JsonProperty("Sell_to_Customer_Name")]
        public string SellToCustomerName { get; set; } = "";

        [JsonProperty("Document_Date")]
        public string DocumentDate { get; set; }

        [JsonProperty("Posting_Date")]
        public string PostingDate { get; set; }

        [JsonProperty("Order_Date")]
        public string OrderDate { get; set; }

        [JsonProperty("Due_Date")]
        public string DueDate { get; set; }

        [JsonProperty("Location_Code")]
        public string LocationCode { get; set; } = "";

        [JsonProperty("Product_Type")]
        public string ProductType { get; set; } = "";

        [JsonProperty("Product_Amount")]
        public long ProductAmount { get; set; }


        

        [JsonProperty("External_Document_No")]
        public string ExternalDocumentNo { get; set; } = "";

        [JsonProperty("Bank")]
        public string Bank { get; set; } = "";

        [JsonProperty("Amount_Paid")]
        public long AmountPaid { get; set; }



    }
}
