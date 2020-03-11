using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Domain.EFCoreEntities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderApi.Contracts.V1;
using OrderApi.Models;
using OrderService.Commands;
using OrderService.Queries;

namespace OrderApi.Controllers
{
    [ApiController]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class OrderApiController : ControllerBase
    {
        private readonly ILogger<OrderApiController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderApiController(ILogger<OrderApiController> logger, IMediator mediator, IMapper mapper)
            => (_logger, _mediator, _mapper) = (logger, mediator, mapper);

        /// <summary>
        /// Returns list of Orders
        /// </summary>
        /// <returns>Returns list of Orders</returns>
        [HttpGet(ApiRoutes.Order.Get)]
        [ProducesResponseType(typeof(List<OrderDetails>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllOrdersQuery();
            var result = await _mediator.Send(query);
            return Ok(_mapper.Map<IEnumerable<OrderDetails>>(result));
        }

        /// <summary>
        /// Returns Order based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order Details</returns>
        [ProducesResponseType(typeof(OrderDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet(ApiRoutes.Order.GetById)]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<OrderDetails>(result));
        }

        /// <summary>
        /// Creates order
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Created Order id</returns>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [HttpPost(ApiRoutes.Order.Create)]
        public async Task<IActionResult> Create(OrderDetails order)
        {
            var query = new CreateOrderCommand(_mapper.Map<Order>(order));
            var result = await _mediator.Send(query);
            return CreatedAtAction("Get", new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates the order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns>Order details</returns>
        [ProducesResponseType(typeof(OrderDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut(ApiRoutes.Order.Update)]
        public async Task<IActionResult> Update(int id, OrderDetails order)
        {
            if (id != order.Id)
                return BadRequest();

            var query = new UpdateOrderCommand(_mapper.Map<Order>(order));
            var result = await _mediator.Send(query);
            return Ok(_mapper.Map<OrderDetails>(result));
        }

        /// <summary>
        /// Delete order based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(ApiRoutes.Order.Delete)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        //[Authorize("CanDelete")]
        [Authorize(Policy = "WorkingInCompany")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = new DeleteOrderCommand(id);
            await _mediator.Send(query);
            return Ok();
        }
    }
}
