using MediatR;
using Microsoft.AspNetCore.Mvc;
using Outbox.Application.Features.Orders.Create;
using Outbox.Application.Features.Orders.GetById;
using Outbox.Application.Features.Orders.Shared;

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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            var orderDto = await mediator.Send(query);
            return orderDto is null
                ? NotFound()
                : Ok(orderDto);
        }
    }
}
