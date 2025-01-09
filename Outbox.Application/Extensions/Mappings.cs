using Outbox.Application.Features.Orders.Create;
using Outbox.Application.Features.Orders.Shared;
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

        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            };
        }
    }
}
