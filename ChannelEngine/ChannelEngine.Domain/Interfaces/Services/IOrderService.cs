using ChannelEngine.Domain.Models;
using ChannelEngine.Domain.Models.ApiResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<MerchantOrderLineResponse>> ListAllInProgressOrderLines();
        Task<IEnumerable<OrderModel>> ListTopProductsSold(int numItems);
        Task<MerchantStockUpdateResponse> UpdateProductStock(string productNo, int stockQuantity);
    }
}
