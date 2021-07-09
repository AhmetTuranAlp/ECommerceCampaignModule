using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Services.OrderService
{
    public interface IOrderService
    {
        Task<string> CreateAsync(string[] operationValue);
    }
}
