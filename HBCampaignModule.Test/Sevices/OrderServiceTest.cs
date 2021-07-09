using HBCampaignModule.Infrastructure.Common;
using HBCampaignModule.Infrastructure.Services.OrderService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HBCampaignModule.Test.Sevices
{
    public class OrderServiceTest
    {
        private readonly IOrderService _orderService;
        private readonly string[] _createAsyncValue = Array.Empty<string>();

        public OrderServiceTest(IOrderService orderService)
        {
            _orderService = orderService;
            string[] createAsyncValue = { "p1", "5" };
            _createAsyncValue = createAsyncValue;

        }


        [Fact]
        public async void CreateAsync_OrderCreate_ReturnString()
        {
            var orderCreate = await _orderService.CreateAsync(_createAsyncValue);
            var response = string.Format(StaticValue.CreateOrderSuccessMessage, _createAsyncValue[0], _createAsyncValue[1]);
            Assert.Equal(orderCreate, response);
        }
    }
}
