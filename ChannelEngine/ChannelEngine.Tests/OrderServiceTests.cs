using ChannelEngine.Domain.Interfaces.Services;
using ChannelEngine.Domain.Models.ApiResponse;
using ChannelEngine.Services.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChannelEngine.Tests
{
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;
        private readonly Mock<IOrderService> _orderServiceMock = new Mock<IOrderService>();

        public OrderServiceTests()
        {
            _orderService = new OrderService();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public async Task ListTopProductsSold_ShouldReturnRecords_LessThanOrEqualToInput(int value)
        {
            // Arrange
            _orderServiceMock.Setup(q => q.ListAllInProgressOrderLines())
                .ReturnsAsync(OrderLinesDummyData());

            // Act
            var topProductsSold = _orderService.ListTopProductsSold(
                await _orderServiceMock.Object.ListAllInProgressOrderLines(), value);

            // Assert
            Assert.True(topProductsSold.Count() <= value);
        }

        [Fact]
        public async Task ListTopProductsSold_ShouldReturnRecords_OrderedByDescendingTotalQuantity()
        {
            // Arrange
            _orderServiceMock.Setup(q => q.ListAllInProgressOrderLines())
                .ReturnsAsync(OrderLinesDummyData());

            // Act
            var topProductsSold = _orderService.ListTopProductsSold(
                await _orderServiceMock.Object.ListAllInProgressOrderLines(), int.MaxValue);

            int maxQuantity = topProductsSold.Max(q => q.TotalQuantity);
            int minQuantity = topProductsSold.Min(q => q.TotalQuantity);

            // Assert
            Assert.True(topProductsSold.First().TotalQuantity == maxQuantity && topProductsSold.Last().TotalQuantity == minQuantity);
        }

        private IEnumerable<MerchantOrderLineResponse> OrderLinesDummyData()
        {
            var orderLines = new List<MerchantOrderLineResponse>
            {
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 1",
                    Gtin = "12345",
                    MerchantProductNo = "P12345",
                    Quantity = 121
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 2",
                    Gtin = "23456",
                    MerchantProductNo = "P23456",
                    Quantity = 133
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 3",
                    Gtin = "34567",
                    MerchantProductNo = "P34567",
                    Quantity = 250
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 4",
                    Gtin = "45678",
                    MerchantProductNo = "P45678",
                    Quantity = 514
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 5",
                    Gtin = "56789",
                    MerchantProductNo = "56789",
                    Quantity = 31
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 6",
                    Gtin = "67890",
                    MerchantProductNo = "P67890",
                    Quantity = 12
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 7",
                    Gtin = "78901",
                    MerchantProductNo = "P78901",
                    Quantity = 2
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 8",
                    Gtin = "89012",
                    MerchantProductNo = "P89012",
                    Quantity = 4
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 9",
                    Gtin = "90123",
                    MerchantProductNo = "P90123",
                    Quantity = 54
                },
                new MerchantOrderLineResponse
                {
                    Description = "Dummy Product 10",
                    Gtin = "01234",
                    MerchantProductNo = "P01234",
                    Quantity = 311
                }
            };

            return orderLines;
        }
    }
}
