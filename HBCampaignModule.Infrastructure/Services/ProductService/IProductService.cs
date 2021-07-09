using HBCampaignModule.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Services.ProductService
{
    public interface IProductService
    {
        Task<string> CreateAsync(string[] operationValue);

        Task<string> GetByIdAsync(string operationValue);

        Task<Product> GetByProductAsync(string operationValue);

        Task<List<string>> GetAll();
    }
}
