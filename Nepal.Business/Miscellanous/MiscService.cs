using AutoMapper;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Data.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service {
    public class MiscService : IMiscService
    {
        private ProductRepository _productRepository;
        private DepotRepository _depotRepository;
        private MarketerRepository _marketerRepository;
        private readonly IMapper _mapper;
        public MiscService(IMapper mapper, ProductRepository productRepository, DepotRepository depotRepository, MarketerRepository marketerRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _depotRepository = depotRepository;
            _marketerRepository = marketerRepository;
        }

        public async Task<List<GenericModel>> GetDepots()
        {
            var depot = await _depotRepository.GetAll();
            return _mapper.Map<List<GenericModel>>(depot);
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            var prod = await _productRepository.GetAll();
            return _mapper.Map<List<ProductModel>>(prod);
        }

        public async Task<MarketerModel> GetMarketer(string UserId)
        {
            var marketer = await _marketerRepository.GetMarketer(UserId);
            return _mapper.Map<MarketerModel>(marketer);

        }
    }
}
