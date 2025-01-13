using Outbox.Domain.Orders;

namespace Outbox.Application.Abstractions.Repositories
{
    public interface IOrdersRepository
    {
        Task<Guid> CreateOrder(Order order);

        Task<Order?> GetOrderById(Guid id);

        Task<int> SaveChangesAsync();
    }
}
