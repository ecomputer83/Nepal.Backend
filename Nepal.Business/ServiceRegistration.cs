using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Nepal.Abstraction.Service.Business;
using Nepal.Abstraction.Service.Data;
using Nepal.Data.Service;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Business.Service
{
    public static class ServiceExtensions
    {
        public static void SetupRegistration(this IServiceCollection services, bool useMockForDatabase, bool useMockForIntegrations)
        {
            //data layer services
            if (useMockForDatabase)
            {
                //services.AddSingleton<ICustomerDataService, MockCustomerDataService>();
            }
            else
            {
                services.AddScoped<ProductRepository>();
                services.AddScoped<DepotRepository>();
                services.AddScoped<MarketerRepository>();
                services.AddScoped<ArticleRepository>();
                services.AddScoped<OrderRepository>();
                services.AddScoped<CreditRepository>();
                services.AddScoped<OrderCreditRepository>();
                services.AddScoped<ProgramRepository>();

            }

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMiscService, MiscService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProgramService, ProgramService>();
            services.AddTransient<ICreditService, CreditService>();
            services.AddTransient<IBlobStoreService, BlobStoreService>();
            
        }
    }
}
