using AutoMapper;
using Nepal.Abstraction.Model;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.Business.Service.ObjectMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ArticleModel, Article>();
            CreateMap<Article, ArticleViewModel>()
                .ForMember(d => d.ImageFile, opt => opt.MapFrom(s => s.ImageUrl));
            CreateMap<OrderModel, Order>();
            CreateMap<Order, OrderViewModel>()
                .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.DepotName, opt => opt.MapFrom(s => s.Depot.Name))
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Abbrev));
            CreateMap<Credit, CreditViewModel>();
            CreateMap<OrderCredit, OrderCreditModel>();
            CreateMap<CreditModel, Credit>();
            CreateMap<Program, ProgramViewModel>()
                .ForMember(d => d.OrderNumber, opt => opt.MapFrom(s => s.Order.OrderNo));
            CreateMap<ProgramModel, Program>();
            CreateMap<RegisterModel, User>().ReverseMap();
            CreateMap<UserManualModel, RegisterModel> ().ReverseMap();
            CreateMap<AdminModel, RegisterModel>();
            CreateMap<Depot, GenericModel>().ReverseMap();
            CreateMap<Marketer, MarketerModel>().ReverseMap();
            CreateMap<MarketerCustomer, MarketerCustomerModel>().ReverseMap();
            CreateMap<User, UserModel>()
                .ForMember(d => d.IsLockout, opt => opt.MapFrom(s => s.LockoutEnabled)).ReverseMap();
            CreateMap<Product, ProductModel>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Abbrev))
                .ForMember(d => d.Unit, opt => opt.MapFrom(s => s.Type))
                .ForMember(d => d.Product, opt => opt.MapFrom(s => s.Abbrev)).ReverseMap();

        }
    }
}
