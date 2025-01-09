namespace Outbox.Domain.Orders
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
