using Orders.Models;
using Orders.Services;

namespace OrdersTests.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void AddOrders_Should_AggregateCorrectly()
        {
            var service = new OrderService();

            var orders = new List<OrderRequest>
        {
            new OrderRequest { ProductId = "456", Quantity = 5 },
            new OrderRequest { ProductId = "456", Quantity = 3 },
            new OrderRequest { ProductId = "789", Quantity = 2 }
        };


            service.AddOrders(orders);

            var aggregatedOrders = service.GetAndClearAggregatedOrders();
             
            Assert.Equal(8, aggregatedOrders["456"]);
            Assert.Equal(2, aggregatedOrders["789"]);
        }
    }
}
