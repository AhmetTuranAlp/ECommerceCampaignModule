using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Domain.Repositories
{
    public interface IProductRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class 
    {
        Task<bool> ProductStockControlAsync(string id, int stock);

        Task<TEntity> GetByProductAsync(string id);
    }
}
