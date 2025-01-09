using MediatR;
using Outbox.Application.Features.Orders.Shared;

namespace Outbox.Application.Features.Orders.GetById
{
    public record GetOrderByIdQuery(Guid Id): IRequest<OrderDto>;
    
}
