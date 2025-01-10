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

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            var orderDto = await mediator.Send(createOrderCommand);
            return CreatedAtAction(nameof(GetOrder), new {id = orderDto.Id }, orderDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
