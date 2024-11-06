using Orders.Models;
using System.Text.Json;

namespace Orders.Services
{
    public interface IOrderService
    {
        void AddOrders(IEnumerable<OrderRequest> orders);
        void Dispose();
    }
    public class OrderService : IOrderService, IDisposable
    {
        private readonly Dictionary<string, int> _orderAggregation = new();
        private readonly Timer? _timer = null;

        public OrderService(bool startTimer = true)
        {
            if (startTimer)
            {
                _timer = new Timer(SendAggregatedOrders, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            }
        }

        public void AddOrders(IEnumerable<OrderRequest> orders)
        {

            foreach (var order in orders)
            {
                if (_orderAggregation.ContainsKey(order.ProductId))
                    _orderAggregation[order.ProductId] += order.Quantity;
                else
                    _orderAggregation[order.ProductId] = order.Quantity;
            }
        }

        public Dictionary<string, int> GetAndClearAggregatedOrders()
        {
            var orders = new Dictionary<string, int>(_orderAggregation);
            _orderAggregation.Clear();
            return orders;
        }

        private void SendAggregatedOrders(object state)
        {
            var aggregatedOrders = GetAndClearAggregatedOrders();

            // Simulate sending orders
            Console.WriteLine("Sending aggregated orders:");
            Console.WriteLine(JsonSerializer.Serialize(aggregatedOrders));
        }

        public void Dispose()
        {
            if (_timer != null)
            _timer.Dispose();
        }
    }
}
