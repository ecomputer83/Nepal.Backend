using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public partial class ClientRequest : BaseRequest
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; } = "";

        [JsonProperty("Name")]
        public string Name { get; set; } = "";

        [JsonProperty("IPMAN_Code")]
        public string IpmanCode { get; set; } = "";

        [JsonProperty("Credit_Limit_LCY")]
        public long CreditLimitLcy { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; } = "";

        [JsonProperty("ContactName")]
        public string ContactName { get; set; } = "";

        [JsonProperty("Phone_No")]
        public string PhoneNo { get; set; } = "";

        [JsonProperty("E_Mail")]
        public string EMail { get; set; } = "";

        [JsonProperty("RC_Number")]
        public string RcNumber { get; set; } = "";

        [JsonProperty("Gen_Bus_Posting_Group")]
        public string GenBusPostingGroup { get; set; } = "LOCAL";
        [JsonProperty("Customer_Posting_Group")]
        public string CustomerPostingGroup { get; set; } = "TRADE";
        [JsonProperty("Customer_Price_Group")]
        public string CustomerPriceGroup { get; set; } = "OGHARA";
    }
}
