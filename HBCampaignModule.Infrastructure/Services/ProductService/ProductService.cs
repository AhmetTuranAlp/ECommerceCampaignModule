using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository<Product> _productRepository;
        public ProductService(IProductRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<string> CreateAsync(string[] operationValues)
        {
            return await _productRepository.CreateAsync(JobHelper.ProductConvert(operationValues));
        }

        public async Task<List<string>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetByIdAsync(string operationValue)
        {
            return await _productRepository.GetByIdAsync(operationValue);
        }

        public async Task<Product> GetByProductAsync(string operationValue)
        {
            return await _productRepository.GetByProductAsync(operationValue);
        }
    }
}
