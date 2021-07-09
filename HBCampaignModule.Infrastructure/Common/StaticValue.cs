using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCampaignModule.Infrastructure.Common
{
    public static class StaticValue
    {
        #region Request
        public static string RequestMessage = "Request: ";
        #endregion

        #region Response
        public static string ResponseMessage = "Response: {0}";
        #endregion

        #region StaticOperationValue
        public static int PriceManipulationLimit = 100;
        #endregion

        #region Operations
        public static string CreateProduct = "create_product";
        public static string GetProductInfo = "get_product_info";
        public static string CreateOrder = "create_order";
        public static string CreateCampaign = "create_campaign";
        public static string GetCampaignInfo = "get_campaign_info";
        public static string IncreaseTime = "increase_time";
        #endregion

        #region Notification
        public static string AnotherAction = "If you want to do a different action, enter the value => 7 : ";
        #endregion

        #region FilePath
        public static string ProductFile = Environment.CurrentDirectory + @"\ProductList";
        public static string ProductFileName = "ProductList.txt";
        public static string OrderFile = Environment.CurrentDirectory + @"\OrderList";
        public static string OrderFileName = "OrderList.txt";
        public static string CampaignFile = Environment.CurrentDirectory + @"\CampaignList";
        public static string CampaignFileName = "CampaignList.txt";
        public static string IncreaseTimeFile = Environment.CurrentDirectory + @"\IncreaseTimeList";
        public static string IncreaseTimeFileName = "IncreaseTimeList.txt";
        #endregion

        #region Error Message
        public static string EqualsProduct = "Product available";
        public static string EqualsOrder = "Order available";
        public static string EqualsCampaign = "Campaign available";
        public static string ProductNotFound = "Product not found";
        public static string OutofStock = "It is not possible to trade with this product. The product is not available or out of stock.";
        public static string CampaignNotFound = "Campaign not found";
        public static string AddingProductFailed = "Adding Product Failed";
        public static string AddingOrderFailed = "Adding Order Failed";
        public static string AddingCampaignFailed = "Adding Campaign Failed";
        public static string ProductNoFile = "No product found";
        public static string PriceManipulationLimitMessage = "Over the limit. Can be up to {0}";
        public static string ErrorMessage = "Operation Failed";
        public static string CampaignAvailable = "Campaign Available";
        public static string CampaignRemainingStock = "The targeted stock of the campaign is insufficient.";
        public static string TimeAdvanceFailed = "Time advance failed";
        #endregion

        #region Success Message
        public static string AddingProductSuccessful = "Adding Product Successful";
        public static string CreateProductSuccessMessage = "Product Created; Code:{0} Price:{1} Stock:{2}";
        public static string GetProductInfoSuccessMessage = "Product {0} Info; Price:{1} Stock:{2}";
        public static string CreateOrderSuccessMessage = "Order Created; Product:{0} Quantity:{1}";
        public static string CreateCampaignSuccessMessage = "Campaign Created; Name:{0} Product:{1} Duration:{2} Limit:{3} Target Sales Count:{4}";
        public static string GetCampaignInfoSuccessMessage = "Campaign {0} Info; Status:{1} Target Sales:{2} Total Sales:{3} Turnover:{4} Average Item Price:{5}";
        public static string IncreaseTimeSuccessMessage = "Time is {0}:00";
        #endregion
    }
}
