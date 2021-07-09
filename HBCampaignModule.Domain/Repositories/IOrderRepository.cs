using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Domain.Repositories
{
   public interface IOrderRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
    }
}
