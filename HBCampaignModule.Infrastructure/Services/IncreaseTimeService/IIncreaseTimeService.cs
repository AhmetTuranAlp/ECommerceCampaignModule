using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Services.IncreaseTimeService
{
    public interface IIncreaseTimeService
    {
        Task<string> TimeAdvanceOperations(string operationValue);
       
    }
}
