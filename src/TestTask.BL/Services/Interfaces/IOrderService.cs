using TestTask.Domain.Entities;

namespace TestTask.BL.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> GetOrder();

        public Task<IEnumerable<Order>> GetOrders();
    }
}