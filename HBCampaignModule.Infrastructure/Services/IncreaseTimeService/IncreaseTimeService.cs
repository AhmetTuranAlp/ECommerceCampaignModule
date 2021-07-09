using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Services.IncreaseTimeService
{
    public class IncreaseTimeService : IIncreaseTimeService
    {
        private readonly IIncreaseTimeRepository<Time> _increaseTimeRepository;
        private readonly IProductRepository<Product> _productRepository;
        private readonly ICampaignRepository<Campaigns> _campaignRepository;
        public IncreaseTimeService(IIncreaseTimeRepository<Time> increaseTimeRepository, IProductRepository<Product> productRepository, ICampaignRepository<Campaigns> campaignRepository)
        {
            _increaseTimeRepository = increaseTimeRepository;
            _productRepository = productRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<string> TimeAdvanceOperations(string operationValue)
        {
            Time time = new Time() { Hour = Convert.ToInt32(operationValue) };
            if (time.Hour > 0)
            {
                var timeSaveMessage = _increaseTimeRepository.CreateAsync(time);
                var ActiveTime = await _increaseTimeRepository.GetTimeAsync();
                if (ActiveTime > 0)
                {
                    List<Campaigns> campaigns = await _campaignRepository.GetAllAsync();
                    foreach (var campaign in campaigns.ToList().Where(x => x.Status == 1))
                    {
                        var product = await _productRepository.GetByProductAsync(campaign.ProductCode);
                        if (product != null)
                        {
                            if (campaign.Duration >= ActiveTime)
                            {
                                var totalDiscountAmount = product.Price * campaign.PriceManipulationLimit / 100; //Toplam İndirim Tutarı
                                var processDiscountAmount = totalDiscountAmount / campaign.Duration; //Süreç İndirimi Miktarı
                                var calculatedDiscountAmount = ActiveTime * processDiscountAmount; //Hesaplanan İndirim Miktarı
                                var currentProductAmount = product.Price - calculatedDiscountAmount; //Güncel Ürün Tutarı

                                product.Price = Math.Round(currentProductAmount, 2);
                                await _productRepository.UpdateAsync(product);
                            }
                            else if (campaign.Duration == ActiveTime)
                            {
                                campaign.Status = 0;
                                await _campaignRepository.UpdateAsync(campaign);

                                product.Price = product.OrjPrice;
                                await _productRepository.UpdateAsync(product);

                            }
                            else
                                return StaticValue.TimeAdvanceFailed;
                        }
                        else
                            return StaticValue.ProductNotFound;
                    }

                    return timeSaveMessage.Result;
                }
                else
                    return StaticValue.TimeAdvanceFailed;
            }
            else
                return StaticValue.TimeAdvanceFailed;
        }
    }
}
