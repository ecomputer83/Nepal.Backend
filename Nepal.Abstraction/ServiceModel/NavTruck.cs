using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public partial class NavTruckResponse
    {

        [JsonProperty("value")]
        public NavTruckValue[] Value { get; set; }
    }

    public partial class NavTruckValue
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("Reg_Number")]
        public string RegNumber { get; set; }

        [JsonProperty("Customer_No")]
        public string CustomerNo { get; set; }

        [JsonProperty("Customer_Name")]
        public string CustomerName { get; set; }

        [JsonProperty("Truck_Description")]
        public string TruckDescription { get; set; }
    }

    public partial class NavTruckRequest : BaseRequest
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; } = "";

        [JsonProperty("Reg_Number")]
        public string RegNumber { get; set; }

        [JsonProperty("Customer_No")]
        public string CustomerNo { get; set; }


        [JsonProperty("Truck_Description")]
        public string TruckDescription { get; set; } = "BRIDGER";
    }
}
