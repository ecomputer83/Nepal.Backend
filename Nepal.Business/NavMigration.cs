using Microsoft.Extensions.Configuration;
using Nepal.Abstraction.Service.Business;
using Nepal.Abstraction.ServiceModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Business.Service
{
    public static class NavMigration
    {
        public static void Migrate(IKYCClientService _KYCClientService, IMiscService _micService)
        {
            // update Product
            var navProducts = _KYCClientService.GetProduct().Result;
            var navDepots = _KYCClientService.GetDepot().Result;

            if(navProducts.Value.Length > 0)
            {
                _micService.InsertorUpdateProduct(navProducts);
            }

            if (navDepots.Value.Length > 0)
            {
                _micService.InsertorUpdateDepot(navDepots);
            }
        }
    } 
}
