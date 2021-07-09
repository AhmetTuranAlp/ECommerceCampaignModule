using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Application.Repositories
{
    public class OrderRepository : IOrderRepository<Order>
    {
        public OrderRepository()
        {
            var fileFullPath = Path.Combine(StaticValue.OrderFile, StaticValue.OrderFileName);

            if (!Directory.Exists(StaticValue.OrderFile))
                Directory.CreateDirectory(StaticValue.OrderFile);

            if (!File.Exists(fileFullPath))
            {
                FileStream file = File.Create(fileFullPath);
                file.Close();
            }
        }

        public async Task<string> CreateAsync(Order entity)
        {
            if (entity != null)
            {
                if (entity.Quantity > 0)
                {
                    var orderList = await ReadData();

                    if (orderList == null)
                        orderList = new List<Order>();

                    if (await WritaData(entity))
                        return string.Format(StaticValue.CreateOrderSuccessMessage, entity.ProductCode, entity.Quantity);
                    else
                        return StaticValue.AddingOrderFailed; 
                }
                else
                    return StaticValue.AddingOrderFailed;
            }
            else
                return StaticValue.ErrorMessage;
        }

        public Task<List<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> ReadData()
        {
            List<Order> orders = new List<Order>();
            try
            {
                var fileFullPath = Path.Combine(StaticValue.OrderFile, StaticValue.OrderFileName);
                string[] readResult = File.ReadAllLines(fileFullPath);
                foreach (string result in readResult)
                {
                    orders.Add(JobHelper.OrderConvert(result.Trim().Split(' ')));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return orders;
        }

        public Task<string> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateData(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> WritaData(Order entity)
        {
            var status = false;
            try
            {
                var fileFullPath = Path.Combine(StaticValue.OrderFile, StaticValue.OrderFileName);
                if (!Directory.Exists(StaticValue.OrderFile))
                    Directory.CreateDirectory(StaticValue.OrderFile);

                if (!File.Exists(fileFullPath))
                {
                    FileStream file = File.Create(fileFullPath);
                    file.Close();
                }

                FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(entity.ProductCode + " " + entity.Quantity.ToString());
                sw.Close();
                status = true;
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
    }
}
