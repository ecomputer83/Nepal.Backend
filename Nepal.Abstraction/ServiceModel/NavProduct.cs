using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public partial class NavProduct
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("No")]
        public string No { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Inventory")]
        public long Inventory { get; set; }

        [JsonProperty("Sales_Unit_of_Measure")]
        public string SalesUnitOfMeasure { get; set; }

        [JsonProperty("Inventory_Posting_Group")]
        public string InventoryPostingGroup { get; set; }

        [JsonProperty("Item_Category_Code")]
        public string ItemCategoryCode { get; set; }

        [JsonProperty("Product_Group_Code")]
        public string ProductGroupCode { get; set; }

        [JsonProperty("Gen_Prod_Posting_Group")]
        public string GenProdPostingGroup { get; set; }

        [JsonProperty("Global_Dimension_1_Filter")]
        public string GlobalDimension1_Filter { get; set; }

        [JsonProperty("Global_Dimension_2_Filter")]
        public string GlobalDimension2_Filter { get; set; }

        [JsonProperty("Location_Filter")]
        public string LocationFilter { get; set; }

        [JsonProperty("Drop_Shipment_Filter")]
        public string DropShipmentFilter { get; set; }

        [JsonProperty("Variant_Filter")]
        public string VariantFilter { get; set; }

        [JsonProperty("Lot_No_Filter")]
        public string LotNoFilter { get; set; }

        [JsonProperty("Serial_No_Filter")]
        public string SerialNoFilter { get; set; }
    }

    public partial class NavProductResponse
    {
        public NavProduct[] Value { get; set; }
    }
}
