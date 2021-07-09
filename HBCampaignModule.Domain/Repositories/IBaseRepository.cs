using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<string> GetByIdAsync(string id);

        Task<List<TEntity>> GetAllAsync();

        Task<string> CreateAsync(TEntity entity);

        Task<string> RemoveAsync(string id);

        Task<bool> UpdateAsync(TEntity entity);

        Task<List<TEntity>> ReadData();

        Task<bool> WritaData(TEntity entity);

    }
}
