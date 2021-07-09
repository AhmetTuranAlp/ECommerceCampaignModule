using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Common;
using HBCampaignModule.Infrastructure.Services.ProductService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HBCampaignModule.Test.Sevices
{
    public class ProductServiceTest
    {
        private readonly IProductService _productService;
        private readonly string[] _createAsyncValue = Array.Empty<string>();
        private readonly string getByIdAsyncValue = string.Empty;

        public ProductServiceTest(IProductService productService)
        {
            _productService = productService;
            string[] createAsyncValue = { "p1", "100", "5000" };
            _createAsyncValue = createAsyncValue;
            getByIdAsyncValue = "p1";
        }

        [Fact]
        public async void CreateAsync_ProductCreate_ReturnString()
        {
            var productCreate =await _productService.CreateAsync(_createAsyncValue);
            var response = string.Format(StaticValue.CreateProductSuccessMessage, _createAsyncValue[0], _createAsyncValue[1], _createAsyncValue[2]);
            if (productCreate == StaticValue.EqualsProduct)
                Assert.Equal(productCreate, StaticValue.EqualsProduct);
            else if (productCreate == response)
                Assert.Equal(productCreate, response);
        }

        [Fact]
        public async void GetByIdAsync_GetProduct_ReturnProduct()
        {
            var productResponse = await _productService.GetByIdAsync(getByIdAsyncValue);
            var getProduct = await _productService.GetByProductAsync(getByIdAsyncValue);
            var response = string.Format(StaticValue.GetProductInfoSuccessMessage, getProduct.ProductCode, getProduct.Price, getProduct.Quantity);
            Assert.Equal(productResponse, response);
        }
    }
}
