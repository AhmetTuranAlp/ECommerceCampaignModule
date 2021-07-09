using HBCampaignModule.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Domain.Model
{
    public class Campaigns : BaseEntity
    {
        public Campaigns()
        {
            Status = 1;
        }
        public string CampaignName { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; } //Süre
        public int PriceManipulationLimit { get; set; } //Fiyat Manipülasyon Limiti
        public int TargetSalesCount { get; set; } //Hedef Satış Sayısı
        public int TotalSalesNumber { get; set; } // Toplam Satış Adedi
        public decimal TotalSalesTurnover { get; set; } // Toplam Satış Cirosu
        public decimal AverageProductPrice { get; set; } // Ortalama Ürün Fiyatı
    }
}
