using HBCampaignModule.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Common
{
    public static class JobHelper
    {
        public static T GetTypeProductItem<T>(string[] values, int index)
        {
            try
            {
                return (T)Convert.ChangeType(values[index], typeof(T));
            }
            catch (Exception ex)
            {
                return Activator.CreateInstance<T>();
            }
        }

        public static Product ProductConvert(string[] operationValues)
        {
            Product product = new Product()
            {
                ProductCode = GetTypeProductItem<string>(operationValues, 0),
                Price = GetTypeProductItem<decimal>(operationValues, 1),
                Quantity = GetTypeProductItem<int>(operationValues, 2),
                OrjPrice = GetTypeProductItem<decimal>(operationValues, 3)
            };
            return product;
        }

        public static Order OrderConvert(string[] operationValues)
        {
            Order order = new Order()
            {
                ProductCode = GetTypeProductItem<string>(operationValues, 0),
                Quantity = GetTypeProductItem<int>(operationValues, 1)
            };
            return order;
        }

        public static Campaigns CampaignConvert(string[] operationValues)
        {
            Campaigns campaigns = new Campaigns()
            {
                CampaignName = GetTypeProductItem<string>(operationValues, 0),
                ProductCode = GetTypeProductItem<string>(operationValues, 1),
                Duration = GetTypeProductItem<int>(operationValues, 2),
                PriceManipulationLimit = GetTypeProductItem<int>(operationValues, 3),
                TargetSalesCount = GetTypeProductItem<int>(operationValues, 4),
                TotalSalesNumber = GetTypeProductItem<int>(operationValues, 5),
                TotalSalesTurnover = GetTypeProductItem<decimal>(operationValues, 6),
                AverageProductPrice = GetTypeProductItem<decimal>(operationValues, 7),
                Status = GetTypeProductItem<int>(operationValues, 8)
            };
            return campaigns;
        }


    }
}
