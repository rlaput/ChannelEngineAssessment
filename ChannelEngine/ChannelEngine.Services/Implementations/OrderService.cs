using ChannelEngine.Domain.Interfaces.Services;
using ChannelEngine.Domain.Models;
using ChannelEngine.Domain.Models.ApiRequest;
using ChannelEngine.Domain.Models.ApiResponse;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Services.Implementations
{
    public class OrderService : IOrderService, IDisposable
    {
        private const string _inProgressStatus = "IN_PROGRESS";
        private const string _apiBaseUrl = "https://api-dev.channelengine.net/api/v2";
        private const string _apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        private readonly RestClient _client;

        public OrderService()
        {
            _client = new RestClient(_apiBaseUrl)
                .AddDefaultQueryParameter("apikey", _apiKey);
        }

        public async Task<IEnumerable<MerchantOrderLineResponse>> ListAllInProgressOrderLines()
        {
            var request = new RestRequest("orders")
                .AddParameter("statuses", _inProgressStatus);

            var response = await _client.GetAsync<CollectionOfMerchantOrderResponse>(request);

            var orderLines = response.Content.SelectMany(q => q.Lines);

            return orderLines;
        }

        public async Task<IEnumerable<OrderModel>> ListTopProductsSold(int numItems)
        {
            var orderLines = await ListAllInProgressOrderLines();

            var orders = orderLines.GroupBy(
                q => new { q.Description, q.Gtin, q.MerchantProductNo },
                q => q.Quantity,
                (key, g) => new OrderModel { ProductName = key.Description, Gtin = key.Gtin, MerchantProductNo = key.MerchantProductNo, TotalQuantity = g.Sum() });

            return orders.OrderByDescending(q => q.TotalQuantity)
                .Take(numItems);
        }

        public async Task<MerchantStockUpdateResponse> UpdateProductStock(string productNo, int stockQuantity)
        {
            var stockUpdateRequest = new MerchantOfferStockUpdateRequest
            {
                MerchantProductNo = productNo,
                StockLocations = new List<MerchantStockLocationUpdateRequest> { new MerchantStockLocationUpdateRequest { Stock = stockQuantity } }
            };

            var request = new RestRequest("offer/stock")
                .AddJsonBody(new List<MerchantOfferStockUpdateRequest> { stockUpdateRequest });

            var response = await _client.PutAsync<MerchantStockUpdateResponse>(request);
            return response;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
