using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public class NavProductPrice
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("Item_No")]
        public string ItemNo { get; set; }

        [JsonProperty("Sales_Type")]
        public string SalesType { get; set; }

        [JsonProperty("Sales_Code")]
        public string SalesCode { get; set; }

        [JsonProperty("Starting_Date")]
        public string StartingDate { get; set; }

        [JsonProperty("Currency_Code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("Variant_Code")]
        public string VariantCode { get; set; }

        [JsonProperty("Unit_of_Measure_Code")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("Minimum_Quantity")]
        public long MinimumQuantity { get; set; }

        [JsonProperty("Ending_Date")]
        public string EndingDate { get; set; }

        [JsonProperty("Unit_Price")]
        public long UnitPrice { get; set; }
    }

    public partial class NavProductPriceResponse
    {
        public NavProductPrice[] Value { get; set; }
    }
}
