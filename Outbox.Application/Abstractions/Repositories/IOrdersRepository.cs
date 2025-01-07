using Outbox.Application.Features.Order.Create;
using Outbox.Domain.Orders;

namespace Outbox.Application.Abstractions.Repositories
{
    public interface IOrdersRepository
    {
        Task<Guid> CreateOrder(Order order);
    }
}
