using Nepal.Abstraction.ServiceModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IKYCClientService
    {
        Task<Client> GetCustomer(string No);
        Task<ClientResponse> GetClient(string EMail);
        Task<NavProductResponse> GetProduct();
        Task<NavDepotResponse> GetDepot();
        Task<NavSaleDetailResponse> GetSaleDetail(string Id);
        Task<NavSaleResponse> GetSales(string ClientNo);
        Task<NavProgramResponse> GetPrograms(string OrderNo);
        Task<NavProgramResponse> GetProgram(List<string> orderNo);
        Task<NavTruckResponse> GetTruck(string TruckNo);
        Task<NavProductPriceResponse> GetPrice();
        Task<NavBankResponse> GetBankAccount();
        Task<NavProgramResponse> GetProgram(string orderNo, string RegNo);
        Task<Client> PostCustomer(ClientRequest request);
        Task<NavSaleDetail> PostOrder(NavSaleRequest request);
        Task<NavProgram> PostProgram(NavProgramRequest request);
        Task<NavTruckValue> PostTruck(NavTruckRequest request);
        Task<NavSaleDetail> PutOrder(string Id, NavSaleRequest request);
        Task<Client> PutCustomer(string Id, ClientRequest request);
    }
}
