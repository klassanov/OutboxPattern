using MediatR;
using Outbox.Application.Abstractions.Repositories;
using Outbox.Application.Extensions;
using Outbox.Application.Features.Orders.Shared;

namespace Outbox.Application.Features.Orders.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrdersRepository ordersRepository;

        public GetOrderByIdQueryHandler(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
           var order =  await ordersRepository.GetOrderById(request.Id);
           if (order == null) return null;

            return order.ToOrderDto();
        }
    }
}
