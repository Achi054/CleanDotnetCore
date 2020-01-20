using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderApi.Models;
using OrderService.Commands;
using OrderService.Queries;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderApiController : ControllerBase
    {
        private readonly ILogger<OrderApiController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderApiController(ILogger<OrderApiController> logger, IMediator mediator, IMapper mapper)
            => (_logger, _mediator, _mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllOrdersQuery();
            var result = await _mediator.Send(query);
            return Ok(_mapper.Map<OrderDetails>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<OrderDetails>(result));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderDetails order)
        {
            var query = new CreateOrderCommand(_mapper.Map<Order>(order));
            var result = await _mediator.Send(query);
            return CreatedAtAction("Get", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderDetails order)
        {
            if (id != order.Id)
                return BadRequest();

            var query = new UpdateOrderCommand(_mapper.Map<Order>(order));
            var result = await _mediator.Send(query);
            return Ok(_mapper.Map<OrderDetails>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = new DeleteOrderCommand(id);
            await _mediator.Send(query);
            return Ok();
        }
    }
}
