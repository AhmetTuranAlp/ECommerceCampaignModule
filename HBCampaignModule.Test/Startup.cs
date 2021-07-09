using Autofac.Extensions.DependencyInjection;
using HBCampaignModule.Application.Repositories;
using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Services.CampaignService;
using HBCampaignModule.Infrastructure.Services.IncreaseTimeService;
using HBCampaignModule.Infrastructure.Services.OrderService;
using HBCampaignModule.Infrastructure.Services.ProductService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.DependencyInjection;

namespace HBCampaignModule.Test
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder) =>
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        public void ConfigureServices(IServiceCollection services) =>
            services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug))
             .AddSingleton<ICampaignRepository<Campaigns>, CampaignRepository>()
            .AddSingleton<IOrderRepository<Order>, OrderRepository>()
            .AddSingleton<IProductRepository<Product>, ProductRepository>()
            .AddSingleton<IIncreaseTimeRepository<Time>, IncreaseTimeRepository>()
            .AddSingleton<IProductService, ProductService>()
            .AddSingleton<IOrderService, OrderService>()
            .AddSingleton<ICampaignService, CampaignService>()
            .AddSingleton<IIncreaseTimeService, IncreaseTimeService>();

    }
}
