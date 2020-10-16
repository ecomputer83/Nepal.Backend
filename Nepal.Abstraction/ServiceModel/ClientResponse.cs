using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Abstraction.ServiceModel
{
    public partial class ClientResponse
    {
        public Client[] Value { get; set; }
    }

    public class Client: BaseRequest
    {
        public string No { get; set; } = "";
        public string Name { get; set; } = "";
        public string ResponsibilityCenter { get; set; } = "";
        public string LocationCode { get; set; } = "";
        public string PostCode { get; set; } = "";
        public string CountryRegionCode { get; set; } = "";
        public string RcNumber { get; set; } = "";
        public string IpmanCode { get; set; } = "";
        public string PhoneNo { get; set; } = "";
        public string Address { get; set; } = "";
        public string EMail { get; set; } = "";
        public string IcPartnerCode { get; set; } = "";
        public string Contact { get; set; } = "";
        public string SalespersonCode { get; set; } = "";
        public string CustomerPostingGroup { get; set; } = "";
        public string GenBusPostingGroup { get; set; } = "";
        public string VatBusPostingGroup { get; set; } = "";
        public string CustomerPriceGroup { get; set; } = "";
        public string CustomerDiscGroup { get; set; } = "";
        public string PaymentTermsCode { get; set; } = "";
        public string ReminderTermsCode { get; set; } = "";
        public string FinChargeTermsCode { get; set; } = "";
        public string CurrencyCode { get; set; } = "";
        public string LanguageCode { get; set; } = "";
        public string SearchName { get; set; } = "";
        public long CreditLimitLcy { get; set; }
        public string Blocked { get; set; } = "";
        public bool PrivacyBlocked { get; set; }
        public string LastDateModified { get; set; }
        public string ApplicationMethod { get; set; } = "";
        public bool CombineShipments { get; set; }
        public string Reserve { get; set; } = "";
        public string ShippingAdvice { get; set; } = "";
        public string ShippingAgentCode { get; set; } = "";
        public string BaseCalendarCode { get; set; } = "";
        public double BalanceLcy { get; set; }
        public double BalanceDueLcy { get; set; }
        public double SalesLcy { get; set; }
        public string GlobalDimension1_Filter { get; set; } = "";
        public string GlobalDimension2_Filter { get; set; } = "";
        public string CurrencyFilter { get; set; } = "";
        public string DateFilter { get; set; } = "";
    }

}
