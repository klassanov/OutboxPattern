using Outbox.Domain.Common;

namespace Outbox.Domain.Orders
{
    public class Order: DomainEntity
    {
        public Order()
        {
            this.Id = Guid.CreateVersion7();
        }

        public Guid Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
