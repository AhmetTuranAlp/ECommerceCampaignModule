using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly IProductRepository<Product> _productRepository;
        private readonly ICampaignRepository<Campaigns> _campaignRepository;
        public OrderService(IOrderRepository<Order> orderRepository, IProductRepository<Product> productRepository, ICampaignRepository<Campaigns> campaignRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _campaignRepository = campaignRepository;
        }


        public async Task<string> CreateAsync(string[] operationValue)
        {
            var order = JobHelper.OrderConvert(operationValue);

            //ürün ve stok kontrolü
            if (await _productRepository.ProductStockControlAsync(order.ProductCode, order.Quantity))
            {
                var product = await _productRepository.GetByProductAsync(order.ProductCode);
                product.Quantity -= order.Quantity;
                //Kampanyaya dahil mi?
                if (await _campaignRepository.IsProductCampaingn(order.ProductCode))
                {
                    var campaign = await _campaignRepository.GetByCampaingnModelAsync(order.ProductCode);
                    if (campaign != null)
                    {
                        //Kalan ürün adedi şipariş işlemindeki adetten fazla ise
                        if ((campaign.TargetSalesCount - campaign.TotalSalesNumber) >= order.Quantity)
                        {
                            campaign.TotalSalesNumber += order.Quantity;
                            campaign.TotalSalesTurnover += product.Price * order.Quantity;
                            campaign.AverageProductPrice = campaign.TotalSalesTurnover / campaign.TotalSalesNumber;
                            if (await _campaignRepository.UpdateAsync(campaign))
                            {
                                if (await _productRepository.UpdateAsync(product))
                                    return await _orderRepository.CreateAsync(order);
                                else
                                    return StaticValue.AddingProductFailed;
                            }
                            else
                                return StaticValue.ErrorMessage;
                        }
                        else
                            return StaticValue.CampaignRemainingStock;
                    }
                    else
                        return StaticValue.CampaignNotFound;
                }
                else
                {
                    if (await _productRepository.UpdateAsync(product))
                        return await _orderRepository.CreateAsync(order);
                    else
                        return StaticValue.AddingProductFailed;
                }
            }
            else
                return StaticValue.OutofStock;
        }
    }
}
