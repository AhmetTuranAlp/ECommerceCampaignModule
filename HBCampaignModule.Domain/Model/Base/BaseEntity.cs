using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Domain.Model.Base
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.Status = 1;
        }
        public int Id { get; set; }
        public int Status { get; set; }
    }
}
