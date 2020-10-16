using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public partial class NavDepotResponse
    {
        public NavDepot[] Value { get; set; }
    }

    public partial class NavDepot
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Sales_Price_Group")]
        public string SalesPriceGroup { get; set; }
    }
}
