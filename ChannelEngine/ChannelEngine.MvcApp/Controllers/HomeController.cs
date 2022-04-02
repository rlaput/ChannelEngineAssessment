using ChannelEngine.Domain.Interfaces.Services;
using ChannelEngine.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;

        public HomeController(ILogger<HomeController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var results = await _orderService.ListTopProductsSold(5);

            var orders = results.Select(q => new OrderViewModel
            {
                Gtin = q.Gtin,
                ProductName = q.ProductName,
                MerchantProductNo = q.MerchantProductNo,
                TotalQuantity = q.TotalQuantity
            });

            return View(orders);
        }

        public async Task<IActionResult> UpdateStock(UpdateStockRequest request)
        {
            var response = await _orderService.UpdateProductStock(request.MerchantProductNo, request.StockQuantity);
            return Json(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
