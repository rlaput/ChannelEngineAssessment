using ChannelEngine.Domain.Interfaces.Services;
using ChannelEngine.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ChannelEngine.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IOrderService, OrderService>()
                .BuildServiceProvider();

            var orderService = serviceProvider.GetService<IOrderService>();

            var topProductsSold = orderService.ListTopProductsSold(5).Result;

            Console.WriteLine("Product Name | GTIN | Total Quantity");
            Console.WriteLine("------------------------------------");

            foreach (var topProduct in topProductsSold)
            {
                Console.WriteLine($"{topProduct.ProductName} | {topProduct.Gtin} | {topProduct.TotalQuantity}");
            }

            string productNo = topProductsSold.Last().MerchantProductNo;
            int stockQuantity = 25;

            Console.WriteLine($"\n\nUpdating stock quantity of {productNo} to {stockQuantity}...");

            var updateResponse = orderService.UpdateProductStock(productNo, stockQuantity).Result;

            Console.WriteLine(updateResponse.Message);

            Console.ReadKey();
        }
    }
}
