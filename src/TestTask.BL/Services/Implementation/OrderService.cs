using Microsoft.Extensions.Logging;
using TestTask.BL.Exceptions;
using TestTask.BL.Services.Interfaces;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Data.Repository.IRepository;

namespace TestTask.BL.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IRepository<Order> orderRepository,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Order> GetOrder()
        {
            _logger.LogInformation("Getting order.");

            var orderWithHighestTotal = await _orderRepository
                .GetOneByAsync(orderBy: q => q.OrderByDescending(order => order.Price * order.Quantity));
            if (orderWithHighestTotal is null)
            {
                _logger.LogWarning("Order not found.");

                throw new OrderNotFoundException($"The base doesn't contain the Order.");
            }

            _logger.LogInformation("Retrieved order successfully.");

            return orderWithHighestTotal;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            _logger.LogInformation("Getting orders.");

            var ordersWithQuantityGreaterThan10 = await _orderRepository
                .GetAllByAsync(include: null, expression: order => order.Quantity > 10);
            if (ordersWithQuantityGreaterThan10 is null)
            {
                _logger.LogWarning("Orders not found.");

                throw new OrderNotFoundException($"The base doesn't contain any orders.");
            }

            _logger.LogInformation("Retrieved all necessary orders successfully.");

            return ordersWithQuantityGreaterThan10;
        }
    }
}
