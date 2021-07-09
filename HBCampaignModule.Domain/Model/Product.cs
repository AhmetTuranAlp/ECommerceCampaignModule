using HBCampaignModule.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Domain.Model
{
    public class Product : BaseEntity
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public decimal OrjPrice { get; set; }
        public int Quantity { get; set; }

    }
}
