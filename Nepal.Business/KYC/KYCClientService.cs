
using Microsoft.Extensions.Configuration;
using Nepal.Abstraction.Service.Business;
using Nepal.Abstraction.ServiceModel;
using Nepal.Business.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service
{
    public class KYCClientService : IKYCClientService
    {

        private Config config { get; set; }
        private IConfiguration configuration;
        public KYCClientService(IConfiguration _configuration)
        {
            configuration = _configuration;
            var obj = configuration.GetSection("NavInfo");
            config = new Config
            {
                Url = obj["Url"],
                UserName = obj["Username"],
                Password = obj["Password"]
            };
        }
        public Task<ClientResponse> GetClient(string EMail)
        {
            
            var resource = $"/Customer?$filter=E_Mail eq '{EMail}'";
            return Task.FromResult(RestService.RestClientCall<ClientResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavProgramResponse> GetProgram(string orderNo, string RegNo)
        {

            var resource = $"/SalesOrderProgramLine?$filter=Document_No eq '{orderNo}' and Truck_No eq '{RegNo}'";
            return Task.FromResult(RestService.RestClientCall<NavProgramResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavProgramResponse> GetProgram(List<string> orderNo)
        {
            var filter = "";
            for (var i= 0; i < orderNo.Count; i++)
            {
                if (i > 0)
                {
                    filter += " or ";
                }
                filter += $"Document_No eq '{orderNo[i]}'";
            }
            var resource = $"/SalesOrderProgramLine?$filter={filter}";
            return Task.FromResult(RestService.RestClientCall<NavProgramResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<Client> GetCustomer(string No)
        {

            var resource = $"/Customer(No='{No}')";
            return Task.FromResult(RestService.RestClientCall<Client>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavProductResponse> GetProduct()
        {
            var resource = $"/PetroleumProducts";
            return Task.FromResult(RestService.RestClientCall<NavProductResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavDepotResponse> GetDepot()
        {
            var resource = $"/Depots";
            return Task.FromResult(RestService.RestClientCall<NavDepotResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavSaleDetailResponse> GetSaleDetail(string Id)
        {
            var resource = $"/SalesOrders?$filter=No eq '{Id}'";
            return Task.FromResult(RestService.RestClientCall<NavSaleDetailResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavSaleResponse> GetSales(string ClientNo)
        {
            var resource = $"/SaleOrder?$filter=Sell_to_Customer_No eq '{ClientNo}'";
            return Task.FromResult(RestService.RestClientCall<NavSaleResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavProgramResponse> GetPrograms(string OrderNo)
        {
            var resource = $"/SalesOrderProgramLine?$filter=Document_No eq '{OrderNo}'";
            return Task.FromResult(RestService.RestClientCall<NavProgramResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }
        public Task<NavTruckResponse> GetTruck(string TruckNo)
        {
            var resource = $"/CustomerTrucks?$filter=Reg_Number eq '{TruckNo}'";
            return Task.FromResult(RestService.RestClientCall<NavTruckResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavProductPriceResponse> GetPrice()
        {
            var resource = $"/SalesPrices";
            return Task.FromResult(RestService.RestClientCall<NavProductPriceResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<NavBankResponse> GetBankAccount()
        {
            var resource = $"/BankAccountList?&filter=Bank_Acc_Posting_Group eq 'BANK NGN'";
            return Task.FromResult(RestService.RestClientCall<NavBankResponse>(this.config, resource, RestSharp.Method.GET, null, true));
        }

        public Task<Client> PostCustomer(ClientRequest request)
        {
            var resource = $"/Customer";
            return Task.FromResult(RestService.RestClientCall<Client>(this.config, resource, RestSharp.Method.POST, request, true));
        }

        public Task<NavSaleDetail> PostOrder(NavSaleRequest request)
        {
            var resource = $"/SalesOrder";
            return Task.FromResult(RestService.RestClientCall<NavSaleDetail>(this.config, resource, RestSharp.Method.POST, request, true));
        }

        public Task<NavProgram> PostProgram(NavProgramRequest request)
        {
            var resource = $"/SalesOrderProgramLine";
            return Task.FromResult(RestService.RestClientCall<NavProgram>(this.config, resource, RestSharp.Method.POST, request, true));
        }
        public Task<NavTruckValue> PostTruck(NavTruckRequest request)
        {
            var resource = $"/CustomerTrucks";
            return Task.FromResult(RestService.RestClientCall<NavTruckValue>(this.config, resource, RestSharp.Method.POST, request, true));
        }
        public Task<NavSaleDetail> PutOrder(string Id, NavSaleRequest request)
        {
            
            var resource = $"/SalesOrder('Order', '{Id}')";
            var resp = RestService.RestClientCall<NavSaleDetail>(this.config, resource, RestSharp.Method.PUT, request, true);
            return Task.FromResult(resp);
        }

        public Task<Client> PutCustomer(string Id, ClientRequest request)
        {

            var resource = $"/Customer(No='{Id}')";
            var resp = RestService.RestClientCall<Client>(this.config, resource, RestSharp.Method.PUT, request, true);
            return Task.FromResult(resp);
        }
    }
    
}
