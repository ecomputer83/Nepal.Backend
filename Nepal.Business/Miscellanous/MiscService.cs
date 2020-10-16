using AutoMapper;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Abstraction.ServiceModel;
using Nepal.Data.Service;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service {
    public class MiscService : IMiscService
    {
        private ProductRepository _productRepository;
        private DepotRepository _depotRepository;
        private MarketerRepository _marketerRepository;
        private SalesPriceRepository _salesPriceRepository;
        private readonly IMapper _mapper;
        private IKYCClientService _kYCClientService;
        public MiscService(IMapper mapper, ProductRepository productRepository, DepotRepository depotRepository,
            MarketerRepository marketerRepository, SalesPriceRepository salesPriceRepository, IKYCClientService kYCClientService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _depotRepository = depotRepository;
            _marketerRepository = marketerRepository;
            _salesPriceRepository = salesPriceRepository;
            _kYCClientService = kYCClientService;
        }

        public async Task<List<DepotModel>> GetDepots()
        {
            var depot = await _depotRepository.GetAll();
            return _mapper.Map<List<DepotModel>>(depot);
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            var prod = await _productRepository.GetAll();
            return _mapper.Map<List<ProductModel>>(prod);
        }

        public async Task<List<BankModel>> GetBanks()
        {
            var prod = await _kYCClientService.GetBankAccount();
            return _mapper.Map<List<BankModel>>(prod.Value.ToList());
        }

        public async Task<List<ProductModel>> GetProducts(string Group)
        {
            List<ProductModel> result = new List<ProductModel>();
            var prod = await _productRepository.GetAll();
            var salesprice = await _kYCClientService.GetPrice();

            foreach(var p in prod)
            {
                var pr = salesprice.Value.FirstOrDefault(s => s.ItemNo == p.Abbrev && s.SalesCode == Group);
                if (pr != null)
                {
                    if (pr.UnitPrice > 0)
                    {
                        var item = _mapper.Map<ProductModel>(p);
                        item.Price = pr.UnitPrice;
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public async Task<List<ProductModel>> GetSalesPrice(int DepotId)
        {
            var prod = await _salesPriceRepository.GetSalesPrice(DepotId);
            return _mapper.Map<List<ProductModel>>(prod);
        }

        public async Task<MarketerModel> GetMarketer(string UserId)
        {
            var marketer = await _marketerRepository.GetMarketer(UserId);
            return _mapper.Map<MarketerModel>(marketer);

        }

        public async Task InsertorUpdateProduct(NavProductResponse nav)
        {
            var navProducts = nav.Value;

            if(navProducts.Length > 0)
            {
                foreach(var product in navProducts)
                {
                    var exist = _productRepository.GetProductByAbbrev(product.No).Result;
                    if(exist == null)
                    {
                        var model = _mapper.Map<Product>(product);
                        model.Type = (product.SalesUnitOfMeasure == "L") ? "ltr" : "kg";

                        _productRepository.Add(model).Wait();
                    }
                }
            }
        }

        public async Task InsertorUpdateDepot(NavDepotResponse nav)
        {
            var navDepots = nav.Value;

            if (navDepots.Length > 0)
            {
                foreach (var depot in navDepots)
                {
                    if (!string.IsNullOrEmpty(depot.SalesPriceGroup))
                    {
                        var exist = _depotRepository.GetDepotByCode(depot.Code).Result;
                        if (exist == null)
                        {
                            var model = _mapper.Map<Depot>(depot);

                            _depotRepository.Add(model).Wait();
                        }
                    }
                }
            }
        }
    }
}
