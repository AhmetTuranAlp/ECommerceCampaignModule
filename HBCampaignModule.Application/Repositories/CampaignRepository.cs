using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HBCampaignModule.Infrastructure.Common;

namespace HBCampaignModule.Application.Repositories
{
    public class CampaignRepository : ICampaignRepository<Campaigns>
    {
        public CampaignRepository()
        {
            var fileFullPath = Path.Combine(StaticValue.CampaignFile, StaticValue.CampaignFileName);

            if (!Directory.Exists(StaticValue.CampaignFile))
                Directory.CreateDirectory(StaticValue.CampaignFile);

            if (!File.Exists(fileFullPath))
            {
                FileStream file = File.Create(fileFullPath);
                file.Close();
            }
        }

        public async Task<string> CreateAsync(Campaigns entity)
        {
            if (entity != null)
            {
                entity.Status = 1;
                if (await WritaData(entity))
                    return string.Format(StaticValue.CreateCampaignSuccessMessage, entity.CampaignName, entity.ProductCode, entity.Duration, entity.PriceManipulationLimit, entity.TargetSalesCount);
                else
                    return StaticValue.AddingCampaignFailed;
            }
            else
                return StaticValue.ErrorMessage;
        }

        public async Task<List<Campaigns>> GetAllAsync()
        {
            List<Campaigns> campaigns = new List<Campaigns>();
            campaigns = await ReadData();
            return campaigns;
        }

        public async Task<Campaigns> GetByCampaingnAsync(string campaingnId)
        {
            var campaignList = await ReadData();

            if (campaignList == null)
                campaignList = new List<Campaigns>();

            if (campaignList.Exists(c => c.CampaignName.ToLower().Equals(campaingnId.ToLower())))
            {
                var campaign = campaignList.FirstOrDefault(x => x.CampaignName.ToLower() == campaingnId.ToLower());
                if (campaign != null)
                    return campaign;
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<Campaigns> GetByCampaingnModelAsync(string productId)
        {
            var campaignList = await ReadData();

            if (campaignList == null)
                campaignList = new List<Campaigns>();

            if (campaignList.Exists(c => c.ProductCode.ToLower().Equals(productId.ToLower())))
            {
                var campaign = campaignList.FirstOrDefault(x => x.ProductCode.ToLower() == productId.ToLower());
                if (campaign != null)
                    return campaign;
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<string> GetByIdAsync(string id)
        {
            var campaignList = await ReadData();

            if (campaignList == null)
                campaignList = new List<Campaigns>();

            if (campaignList.Exists(c => c.CampaignName.ToLower().Equals(id.ToLower())))
            {
                var campaign = campaignList.FirstOrDefault(x => x.CampaignName.ToLower() == id.ToLower());
                var statusMessage = campaign.Status == 1 ? "Active" : "Passive";

                return string.Format(StaticValue.GetCampaignInfoSuccessMessage, campaign.CampaignName, statusMessage, campaign.TargetSalesCount, campaign.TotalSalesNumber, campaign.TotalSalesTurnover, campaign.AverageProductPrice);
            }
            else
                return StaticValue.ProductNotFound;
        }

        public async Task<bool> IsProductCampaingn(string productId)
        {
            var campaignList = await ReadData();

            if (campaignList == null)
                campaignList = new List<Campaigns>();

            if (campaignList.Exists(c => c.ProductCode.ToLower().Equals(productId.ToLower())))
            {
                var campaign = campaignList.FirstOrDefault(x => x.ProductCode.ToLower() == productId.ToLower());
                if (campaign != null)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<bool> IsThereCampaign(string id)
        {
            var campaignList = await ReadData();

            if (campaignList == null)
                campaignList = new List<Campaigns>();

            if (campaignList.Exists(c => c.CampaignName.ToLower().Equals(id.ToLower())))
                return true;
            else
                return false;
        }

        public async Task<List<Campaigns>> ReadData()
        {
            List<Campaigns> campaigns = new List<Campaigns>();
            try
            {
                var fileFullPath = Path.Combine(StaticValue.CampaignFile, StaticValue.CampaignFileName);
                string[] readResult = File.ReadAllLines(fileFullPath);
                foreach (string result in readResult)
                {
                    campaigns.Add(JobHelper.CampaignConvert(result.Trim().Split(' ')));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return campaigns;
        }

        public Task<string> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Campaigns entity)
        {
            var status = false;
            try
            {
                var campaignList = await ReadData();
                if (campaignList != null)
                {
                    foreach (var campaign in campaignList)
                    {
                        if (campaign.CampaignName.ToLower() == entity.CampaignName.ToLower())
                        {
                            campaign.AverageProductPrice = entity.AverageProductPrice;
                            campaign.CampaignName = entity.CampaignName;
                            campaign.Duration = entity.Duration;
                            campaign.Id = entity.Id;
                            campaign.PriceManipulationLimit = entity.PriceManipulationLimit;
                            campaign.ProductCode = entity.ProductCode;
                            campaign.Status = entity.Status;
                            campaign.TargetSalesCount = entity.TargetSalesCount;
                            campaign.TotalSalesNumber = entity.TotalSalesNumber;
                            campaign.TotalSalesTurnover = entity.TotalSalesTurnover;
                        }
                    }

                    var fileFullPath = Path.Combine(StaticValue.CampaignFile, StaticValue.CampaignFileName);
                    if (File.Exists(fileFullPath))
                        File.Delete(fileFullPath);

                    foreach (var campaignFile in campaignList)
                        await WritaData(campaignFile);

                    status = true;

                }
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }

        public async Task<bool> WritaData(Campaigns entity)
        {
            var status = false;
            try
            {
                var fileFullPath = Path.Combine(StaticValue.CampaignFile, StaticValue.CampaignFileName);
                if (!Directory.Exists(StaticValue.CampaignFile))
                    Directory.CreateDirectory(StaticValue.CampaignFile);

                if (!File.Exists(fileFullPath))
                {
                    FileStream file = File.Create(fileFullPath);
                    file.Close();
                }

                FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(
                    entity.CampaignName + " " +
                    entity.ProductCode + " " +
                    entity.Duration.ToString() + " " +
                    entity.PriceManipulationLimit.ToString() + " " +
                    entity.TargetSalesCount.ToString() + " " +
                    entity.TotalSalesNumber + " " +
                    entity.TotalSalesTurnover + " " +
                    Math.Round(entity.AverageProductPrice, 2) + " " +
                    entity.Status
                    );
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
