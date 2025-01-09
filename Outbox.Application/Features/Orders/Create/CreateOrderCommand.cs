using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Outbox.Application.Features.Orders.Create
{
    public record CreateOrderCommand : IRequest<OrderDto>
    {
        public int CustomerId { get; init; }

        public int ProductId { get; init; }

        public int Quantity { get; init; }
    }
}
