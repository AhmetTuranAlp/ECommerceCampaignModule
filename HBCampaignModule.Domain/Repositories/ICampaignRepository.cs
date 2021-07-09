using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Domain.Repositories
{
    public interface ICampaignRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Sipariş verilen ürün bir kampanyaya dahilmi kontrol ediliyor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<bool> IsProductCampaingn(string productId);

        /// <summary>
        /// Kampanya mevcut mu?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsThereCampaign(string id);

        /// <summary>
        /// Kampanya datasını model olarak döner.
        /// </summary>
        /// <param name="campaingnId"></param>
        /// <returns></returns>
        Task<TEntity> GetByCampaingnAsync(string campaingnId);

        /// <summary>
        /// Kampanya datasını model olarak döner.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<TEntity> GetByCampaingnModelAsync(string productId);


    }
}
