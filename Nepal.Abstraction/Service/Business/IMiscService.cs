using Nepal.Abstraction.Model;
using Nepal.Abstraction.ServiceModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IMiscService
    {
        Task<List<ProductModel>> GetSalesPrice(int DepotId);
        Task<List<ProductModel>> GetProducts();
        Task<List<ProductModel>> GetProducts(string Group);
        Task<List<DepotModel>> GetDepots();
        Task<List<BankModel>> GetBanks();
        Task<MarketerModel> GetMarketer(string UserId);
        Task InsertorUpdateProduct(NavProductResponse nav);
        Task InsertorUpdateDepot(NavDepotResponse nav);
    }
}
