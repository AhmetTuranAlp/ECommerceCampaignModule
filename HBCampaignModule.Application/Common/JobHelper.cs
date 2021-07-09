using HBCampaignModule.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Application.Common
{
    public class JobHelper
    {
        public T GetTypeProductItem<T>(string[] values, int index)
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

        public Product ProductConvert(string[] operationValues)
        {
            string productCode = GetTypeProductItem<string>(operationValues, 0);
            decimal price = GetTypeProductItem<int>(operationValues, 1);
            int quantity = GetTypeProductItem<int>(operationValues, 2);

            Product product = new Product()
            {
                ProductCode = productCode,
                Price = price,
                Quantity = quantity
            };
            return product;
        }
    }
}
