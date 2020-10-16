using AutoMapper;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.ServiceModel;
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
                .ForMember(d => d.OrderNumber, opt => opt.MapFrom(s => s.Order.OrderNo))
                .ForMember(d => d.ProgramDate, opt => opt.MapFrom(s => s.CreatedOn.ToLongDateString()));
            CreateMap<ProgramModel, Program>();
            CreateMap<RegisterModel, User>().ReverseMap();
            CreateMap<UserManualModel, RegisterModel>().ReverseMap();
            CreateMap<AdminModel, RegisterModel>();
            CreateMap<Depot, DepotModel>().ReverseMap();
            CreateMap<Marketer, MarketerModel>().ReverseMap();
            CreateMap<MarketerCustomer, MarketerCustomerModel>().ReverseMap();
            CreateMap<User, UserModel>()
                .ForMember(d => d.IsLockout, opt => opt.MapFrom(s => s.LockoutEnabled)).ReverseMap();
            CreateMap<Product, ProductModel>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Abbrev))
                .ForMember(d => d.Unit, opt => opt.MapFrom(s => s.Type))
                .ForMember(d => d.Product, opt => opt.MapFrom(s => s.Abbrev)).ReverseMap();
            CreateMap<SalesPrice, ProductModel>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Product.Abbrev))
                .ForMember(d => d.Unit, opt => opt.MapFrom(s => s.Product.Type))
                .ForMember(d => d.Product, opt => opt.MapFrom(s => s.Product.Abbrev)).ReverseMap();
            CreateMap<User, ClientRequest>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.BusinessName))
                .ForMember(d => d.RcNumber, opt => opt.MapFrom(s => s.RCNumber))
                .ForMember(d => d.EMail, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNo, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.ContactName, opt => opt.MapFrom(s => s.ContactName))
                .ForMember(d => d.PhoneNo, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.CreditLimitLcy, opt => opt.MapFrom(s => long.Parse(s.CreditLimit)))
                .ForMember(d => d.IpmanCode, opt => opt.MapFrom(s => s.IPMANCode)).ReverseMap();
            CreateMap<Order, NavSaleRequest>()
                .ForMember(d => d.SellToCustomerName, opt => opt.MapFrom(s => s.User.BusinessName))
                .ForMember(d => d.SellToCustomerNo, opt => opt.MapFrom(s => s.User.UserNo))
                .ForMember(d => d.ProductAmount, opt => opt.MapFrom(s => s.TotalAmount)).ReverseMap();
            CreateMap<Program, NavProgramRequest>()
                .ForMember(d => d.No, opt => opt.MapFrom(s => s.Order.Product.Abbrev))
                .ForMember(d => d.DocumentNo, opt => opt.MapFrom(s => s.Order.OrderNo))
                .ForMember(d => d.QtyToShip, opt => opt.MapFrom(s => s.Quantity)).ReverseMap();
            CreateMap<Product, NavProduct>()
                .ForMember(d => d.InventoryPostingGroup, opt => opt.MapFrom(s => s.Abbrev))
                .ForMember(d => d.No, opt => opt.MapFrom(s => s.Abbrev))
                .ForMember(d => d.InventoryPostingGroup, opt => opt.MapFrom(s => s.Name)).ReverseMap();
            CreateMap<NavSaleDetail, NavSaleRequest>().ReverseMap();
            CreateMap<NavBank, BankModel>().ReverseMap();
            CreateMap<Client, ClientRequest>().ReverseMap();
            CreateMap<Depot, NavDepot>()
                .ForMember(d => d.SalesPriceGroup, opt => opt.MapFrom(s => s.Group)).ReverseMap();
        }
    }
}
