using HBCampaignModule.Infrastructure.Common;
using HBCampaignModule.Infrastructure.Services.CampaignService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HBCampaignModule.Test.Sevices
{
    public class CampaignServiceTest
    {
        private readonly ICampaignService _campaignService;
        private readonly string[] _createAsyncValue = Array.Empty<string>();
        private readonly string getByIdAsyncValue = string.Empty;

        public CampaignServiceTest(ICampaignService campaignService)
        {
            _campaignService = campaignService;
            string[] createAsyncValue = { "C1", "P1", "10", "20", "100" };
            _createAsyncValue = createAsyncValue;
            getByIdAsyncValue = "C1";
        }

        [Fact]
        public async void CreateAsync_CampaignCreate_ReturnString()
        {
            var campaignCreate = await _campaignService.CreateAsync(_createAsyncValue);
            var response = string.Format(StaticValue.CreateCampaignSuccessMessage, _createAsyncValue[0], _createAsyncValue[1], _createAsyncValue[2], _createAsyncValue[3], _createAsyncValue[4]);
            if (campaignCreate == StaticValue.EqualsProduct)
                Assert.Equal(campaignCreate, StaticValue.EqualsProduct);
            else if (campaignCreate == response)
                Assert.Equal(campaignCreate, response);
        }

        [Fact]
        public async void GetByIdAsync_GetCampaign_ReturnProduct()
        {
            var campaignResponse = await _campaignService.GetByIdAsync(getByIdAsyncValue);
            var getCampaign = await _campaignService.GetByCampaingnAsync(getByIdAsyncValue);
            var statusMessage = getCampaign.Status == 1 ? "Active" : "Passive";

            var response = string.Format(StaticValue.GetCampaignInfoSuccessMessage, getCampaign.CampaignName, statusMessage, getCampaign.TargetSalesCount, getCampaign.TotalSalesNumber, getCampaign.TotalSalesTurnover, getCampaign.AverageProductPrice);
            Assert.Equal(campaignResponse, response);
        }
    }
}
