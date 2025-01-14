using Outbox.Domain.Common;

namespace Outbox.Domain.Orders
{
    public record OrderCreatedEvent(Guid OrderId, int CustomerId, int ProductId, int Quantity) : IDomainEvent;
    
}
