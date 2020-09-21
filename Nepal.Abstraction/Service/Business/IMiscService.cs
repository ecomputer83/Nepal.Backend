using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IMiscService
    {
        Task<List<ProductModel>> GetProducts();
        Task<List<GenericModel>> GetDepots();
        Task<MarketerModel> GetMarketer(string UserId);
    }
}
