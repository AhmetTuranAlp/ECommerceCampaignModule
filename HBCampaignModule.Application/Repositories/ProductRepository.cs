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
    public class ProductRepository : IProductRepository<Product>
    {
        public ProductRepository()
        {
            var fileFullPath = Path.Combine(StaticValue.ProductFile, StaticValue.ProductFileName);

            if (!Directory.Exists(StaticValue.ProductFile))
                Directory.CreateDirectory(StaticValue.ProductFile);

            if (!File.Exists(fileFullPath))
            {
                FileStream file = File.Create(fileFullPath);
                file.Close();
            }
        }

        public async Task<string> CreateAsync(Product entity)
        {
            if (entity != null)
            {
                if (entity.Price > 0 && entity.Quantity > 0)
                {
                    var productList = await ReadData();

                    if (productList == null)
                        productList = new List<Product>();

                    if (productList.Exists(c => c.ProductCode.ToLower().Equals(entity.ProductCode.ToLower())))
                        return StaticValue.EqualsProduct;

                    entity.OrjPrice = entity.Price;

                    if (await WritaData(entity))
                        return string.Format(StaticValue.CreateProductSuccessMessage, entity.ProductCode, entity.Price, entity.Quantity);
                    else
                        return StaticValue.AddingProductFailed;
                }
                return StaticValue.AddingProductFailed;
            }
            return StaticValue.ErrorMessage;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetByIdAsync(string id)
        {
            var productList = await ReadData();

            if (productList == null)
                productList = new List<Product>();

            if (productList.Exists(c => c.ProductCode.ToLower().Equals(id.ToLower())))
            {
                var product = productList.FirstOrDefault(x => x.ProductCode.ToLower() == id.ToLower());
                return string.Format(StaticValue.GetProductInfoSuccessMessage, product.ProductCode, product.Price, product.Quantity);
            }
            else
                return StaticValue.ProductNotFound;
        }

        public Task<string> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }



        public async Task<List<Product>> ReadData()
        {
            List<Product> products = new List<Product>();
            try
            {
                var fileFullPath = Path.Combine(StaticValue.ProductFile, StaticValue.ProductFileName);
                string[] readResult = File.ReadAllLines(fileFullPath);
                foreach (string result in readResult)
                {
                    products.Add(JobHelper.ProductConvert(result.Trim().Split(' ')));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return products;
        }

        public async Task<bool> WritaData(Product entity)
        {
            var status = false;
            try
            {
                var fileFullPath = Path.Combine(StaticValue.ProductFile, StaticValue.ProductFileName);
                if (!Directory.Exists(StaticValue.ProductFile))
                    Directory.CreateDirectory(StaticValue.ProductFile);

                if (!File.Exists(fileFullPath))
                {
                    FileStream file = File.Create(fileFullPath);
                    file.Close();
                }

                FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(entity.ProductCode + " " + entity.Price.ToString() + " " + entity.Quantity.ToString() + " " + entity.OrjPrice.ToString());
                sw.Close();
                status = true;
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }

        public async Task<bool> ProductStockControlAsync(string id, int stock)
        {
            var productList = await ReadData();

            if (productList == null)
                productList = new List<Product>();

            if (productList.Exists(c => c.ProductCode.ToLower().Equals(id.ToLower())))
            {
                var product = productList.FirstOrDefault(x => x.ProductCode.ToLower() == id.ToLower());
                if (product != null)
                {
                    if (product.Quantity >= stock)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }


        public async Task<Product> GetByProductAsync(string id)
        {
            var productList = await ReadData();

            if (productList == null)
                productList = new List<Product>();

            if (productList.Exists(c => c.ProductCode.ToLower().Equals(id.ToLower())))
            {
                var product = productList.FirstOrDefault(x => x.ProductCode.ToLower() == id.ToLower());
                if (product != null)
                    return product;
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<bool> UpdateAsync(Product entity)
        {
            var status = false;
            try
            {
                var productList = await ReadData();
                if (productList.Count > 0)
                {
                    foreach (var product in productList)
                    {
                        if (product.ProductCode.ToLower() == entity.ProductCode.ToLower())
                        {
                            product.OrjPrice = entity.OrjPrice;
                            product.Price = entity.Price;
                            product.ProductCode = entity.ProductCode;
                            product.Quantity = entity.Quantity;
                        }

                    }

                    var fileFullPath = Path.Combine(StaticValue.ProductFile, StaticValue.ProductFileName);
                    if (File.Exists(fileFullPath))
                        File.Delete(fileFullPath);

                    foreach (var productFile in productList)
                        await WritaData(productFile);

                    status = true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
    }
}
