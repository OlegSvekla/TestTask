using Microsoft.EntityFrameworkCore;
using TestTask.Data.IRepository;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementation
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
                .GetAllAsync()
                .OrderByDescending(order => order.Price * order.Quantity)
                .FirstOrDefaultAsync();

            return orderWithHighestTotal;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var ordersWithQuantityGreaterThan10 = await _orderRepository.GetAllByAsync(
                include: null,
                expression: order => order.Quantity > 10
            );

            return ordersWithQuantityGreaterThan10;
        }
    }
}
