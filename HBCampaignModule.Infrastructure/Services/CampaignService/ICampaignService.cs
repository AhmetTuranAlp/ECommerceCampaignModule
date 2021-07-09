using HBCampaignModule.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Services.CampaignService
{
    public interface ICampaignService
    {
        Task<string> CreateAsync(string[] operationValue);

        Task<string> GetByIdAsync(string operationValue);

        Task<Campaigns> GetByCampaingnAsync(string operationValue);
    }
}
