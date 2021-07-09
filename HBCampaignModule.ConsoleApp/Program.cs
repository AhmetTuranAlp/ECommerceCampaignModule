using System;
using System.Linq;
using System.Threading.Tasks;
using HBCampaignModule.Application.Repositories;
using HBCampaignModule.Domain.Model;
using HBCampaignModule.Domain.Repositories;
using HBCampaignModule.Infrastructure.Common;
using HBCampaignModule.Infrastructure.Services.CampaignService;
using HBCampaignModule.Infrastructure.Services.IncreaseTimeService;
using HBCampaignModule.Infrastructure.Services.OrderService;
using HBCampaignModule.Infrastructure.Services.ProductService;
using Microsoft.Extensions.DependencyInjection;

namespace HBCampaignModule.ConsoleApp
{
    class Program
    {
        private readonly static string[] operations = { "0", "1", "2", "3", "4", "5", "6" };

        private static string PrintModeTable()
        {
            Console.Clear();

            int tableLeft = 40;
            int tableRight = 30;
            Console.WriteLine("Console Version : 1\n\n");
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", getSeperator(tableLeft), getSeperator(tableRight)));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "Çalışma Modu", "Girilmesi Gereken Değer"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", getSeperator(tableLeft), getSeperator(tableRight)));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "create_product", "1"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "get_product_info", "2"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "create_order", "3"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "create_campaign", "4"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "get_campaign_info", "5"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "increase_time", "6"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", getSeperator(tableLeft), getSeperator(tableRight)));

            Console.WriteLine("\n\nLütfen çalışma modu için tablodan bir değer giriniz:");
            return Console.ReadLine();
        }

        public static string getSeperator(int length)
        {
            string seperator = string.Empty;
            for (int i = 0; i < length; i++)
            {
                seperator += "-";
            }
            return seperator;
        }

        private static ServiceProvider DependencyInjection()
        {
            var serviceDescriptors = new ServiceCollection()
            .AddSingleton<ICampaignRepository<Campaigns>, CampaignRepository>()
            .AddSingleton<IOrderRepository<Order>, OrderRepository>()
            .AddSingleton<IProductRepository<Product>, ProductRepository>()
            .AddSingleton<IIncreaseTimeRepository<Time>, IncreaseTimeRepository>()
            .AddSingleton<IProductService, ProductService>()
            .AddSingleton<IOrderService, OrderService>()
            .AddSingleton<ICampaignService, CampaignService>()
            .AddSingleton<IIncreaseTimeService, IncreaseTimeService>()
            .BuildServiceProvider();
            return serviceDescriptors;
        }

        public static async void Operations(ServiceProvider provider)
        {
            try
            {
                string workMode = string.Empty;
                do
                {
                    Console.Title = "CampaignModule";
                    var workValues = string.Empty;
                    do
                    {
                        workMode = PrintModeTable();
                    }
                    while (!operations.Contains(workMode));

                    Console.Clear();
                    switch (workMode)
                    {
                        case "1":
                            Console.Title = StaticValue.CreateProduct; Console.Write(StaticValue.RequestMessage); workValues = Console.ReadLine();
                            var createProduct = provider.GetService<IProductService>();
                            Console.WriteLine(string.Format(StaticValue.ResponseMessage, await createProduct.CreateAsync(workValues.Trim().Split(' '))));
                            break;
                        case "2":
                            Console.Title = StaticValue.GetProductInfo; Console.Write(StaticValue.RequestMessage); workValues = Console.ReadLine();
                            var getProductInfo = provider.GetService<IProductService>();
                            Console.WriteLine(string.Format(StaticValue.ResponseMessage, await getProductInfo.GetByIdAsync(workValues)));
                            break;
                        case "3":
                            Console.Title = StaticValue.CreateOrder; Console.Write(StaticValue.RequestMessage); workValues = Console.ReadLine();
                            var createOrder = provider.GetService<IOrderService>();
                            Console.WriteLine(string.Format(StaticValue.ResponseMessage, await createOrder.CreateAsync(workValues.Trim().Split(' '))));
                            break;
                        case "4":
                            Console.Title = StaticValue.CreateCampaign; Console.Write(StaticValue.RequestMessage); workValues = Console.ReadLine();
                            var createCampaign = provider.GetService<ICampaignService>();
                            Console.WriteLine(string.Format(StaticValue.ResponseMessage, await createCampaign.CreateAsync(workValues.Trim().Split(' '))));
                            break;
                        case "5":
                            Console.Title = StaticValue.GetCampaignInfo; Console.Write(StaticValue.RequestMessage); workValues = Console.ReadLine();
                            var getCampaignInfo = provider.GetService<ICampaignService>();
                            Console.WriteLine(string.Format(StaticValue.ResponseMessage, await getCampaignInfo.GetByIdAsync(workValues)));
                            break;
                        case "6":
                            Console.Title = StaticValue.IncreaseTime; Console.Write(StaticValue.RequestMessage); workValues = Console.ReadLine();
                            var increaseTime = provider.GetService<IIncreaseTimeService>();
                            Console.WriteLine(string.Format(StaticValue.ResponseMessage, await increaseTime.TimeAdvanceOperations(workValues)));
                            break;
                        default:
                            break;
                    }
                    Console.Write(StaticValue.AnotherAction); workMode = Console.ReadLine();
                }
                while (workMode == "7");
            }
            catch (Exception)
            {
                Console.WriteLine(StaticValue.ErrorMessage);
            }

        }
        static void Main(string[] args)
        {
            Operations(DependencyInjection());
        }
    }
}
