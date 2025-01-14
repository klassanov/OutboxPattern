using MediatR;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Application.Extensions;
using Outbox.Application.Features.Orders.Shared;
using Outbox.Domain.Orders;

namespace Outbox.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrdersRepository ordersRepository;

        public CreateOrderCommandHandler(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = request.ToOrder();
            order.Raise(new OrderCreatedEvent(order.Id, order.CustomerId, order.ProductId, order.Quantity));
            var orderId = await ordersRepository.CreateOrder(order);

            await ordersRepository.SaveChangesAsync();

            return new OrderDto()
            {
                Id = orderId,
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                ProductName = "My Product Name"
            };
        }
    }
}
