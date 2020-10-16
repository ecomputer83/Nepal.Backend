using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public class NavBank
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("No")]
        public string No { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Currency_Code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("OnlineFeedStatementStatus")]
        public string OnlineFeedStatementStatus { get; set; }

        [JsonProperty("Post_Code")]
        public string PostCode { get; set; }

        [JsonProperty("Country_Region_Code")]
        public string CountryRegionCode { get; set; }

        [JsonProperty("Phone_No")]
        public string PhoneNo { get; set; }

        [JsonProperty("Fax_No")]
        public string FaxNo { get; set; }

        [JsonProperty("Contact")]
        public string Contact { get; set; }

        [JsonProperty("Bank_Account_No")]
        public string BankAccountNo { get; set; }

        [JsonProperty("SWIFT_Code")]
        public string SwiftCode { get; set; }

        [JsonProperty("IBAN")]
        public string Iban { get; set; }

        [JsonProperty("Our_Contact_Code")]
        public string OurContactCode { get; set; }

        [JsonProperty("Bank_Acc_Posting_Group")]
        public string BankAccPostingGroup { get; set; }

        [JsonProperty("Language_Code")]
        public string LanguageCode { get; set; }

        [JsonProperty("Search_Name")]
        public string SearchName { get; set; }
    }

    public class NavBankResponse
    {
        public NavBank[] Value { get; set; }
    }
}
