using MediatR;
using Microsoft.AspNetCore.Mvc;
using Outbox.Application.Features.Orders.Create;

namespace Outbox.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<OrderDto> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            var order = await mediator.Send(createOrderCommand);
            return order;
        }
    }
}
