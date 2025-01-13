using Outbox.Domain.Common;

namespace Outbox.Domain.Orders
{
    public record OrderCreatedEvent(Guid OrderId): IDomainEvent;
    
}
