using TestTask.BL.Services.Interfaces;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Data.Repository.IRepository;

namespace TestTask.BL.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrder()
        {
            var orderWithHighestTotal = await _orderRepository
                .GetOneByAsync(orderBy: q => q.OrderByDescending(order => order.Price * order.Quantity));

            return orderWithHighestTotal;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var ordersWithQuantityGreaterThan10 = await _orderRepository
                .GetAllByAsync(include: null, expression: order => order.Quantity > 10
            );

            return ordersWithQuantityGreaterThan10;
        }
    }
}
