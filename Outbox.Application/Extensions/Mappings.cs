using Outbox.Application.Features.Orders.Create;
using Outbox.Domain.Orders;

namespace Outbox.Application.Extensions
{
    public static class Mappings
    {
        public static Order ToOrder(this CreateOrderCommand command)
        {
            return new Order()
            {                
                CustomerId = command.CustomerId,
                ProductId = command.ProductId,
                Quantity = command.Quantity
            };
        }
    }
}
