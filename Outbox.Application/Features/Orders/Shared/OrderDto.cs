using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outbox.Application.Features.Orders.Shared
{
    public record OrderDto
    {
        public Guid Id { get; set; }

        public int ProductId { get; init; }

        public string? ProductName { get; init; }

        public int Quantity { get; init; }

        public int CustomerId { get; init; }
    }
}
