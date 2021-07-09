using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Common;

namespace HBCampaignModule.Infrastructure.Services.CampaignService
{
    public class CampaignService : ICampaignService
    {
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly IProductRepository<Product> _productRepository;
        private readonly ICampaignRepository<Campaigns> _campaignRepository;
        public CampaignService(IOrderRepository<Order> orderRepository, IProductRepository<Product> productRepository, ICampaignRepository<Campaigns> campaignRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<string> CreateAsync(string[] operationValue)
        {
            var campaign = JobHelper.CampaignConvert(operationValue);
            //ürün ve stok kontrolü
            if (await _productRepository.ProductStockControlAsync(campaign.ProductCode, campaign.TargetSalesCount))
            {
                var product = await _productRepository.GetByProductAsync(campaign.ProductCode);
                //Kampanya yok ise
                if (!await _campaignRepository.IsThereCampaign(campaign.CampaignName))
                {
                    //İndirim oranı 100'den fazla olamaz.
                    if (campaign.PriceManipulationLimit <= StaticValue.PriceManipulationLimit)
                        return await _campaignRepository.CreateAsync(campaign);
                    else
                        return string.Format(StaticValue.PriceManipulationLimitMessage, StaticValue.PriceManipulationLimit);
                }
                else
                    return StaticValue.CampaignAvailable;
            }
            else
                return StaticValue.OutofStock;

        }

        public async Task<Campaigns> GetByCampaingnAsync(string operationValue)
        {
            return await _campaignRepository.GetByCampaingnAsync(operationValue);
        }

        public async Task<string> GetByIdAsync(string operationValue)
        {
            return await _campaignRepository.GetByIdAsync(operationValue);
        }
    }
}
