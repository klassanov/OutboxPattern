namespace Outbox.Domain.Orders
{
    public class Order
    {
        public Guid Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
