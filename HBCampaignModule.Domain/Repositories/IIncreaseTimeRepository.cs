using HBCampaignModule.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Domain.Repositories
{
    public interface IIncreaseTimeRepository<TEntity>
    {
        Task<string> CreateAsync(TEntity entity);

        Task<TEntity> ReadData();

        Task<bool> WritaData(TEntity entity);

        Task<bool> UpdateData(TEntity entity);

        Task<int> GetTimeAsync();
    }
}
