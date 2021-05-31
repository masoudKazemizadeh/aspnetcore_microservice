using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.Features.Commands.DeleteOrder;
using Ordering.Application.Features.Commands.UpdateOrder;
using Ordering.Application.Features.Queries.GetOrderList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/Order")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}", Name = "GetOrderListByUsername")]
        [ProducesResponseType(typeof(OrderVm), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderByUserName(string username)
        {
            var query = new GetOrderListQuery(username);
            return Ok(await _mediator.Send(query));
        }

        //for test
        //it will be triggered by rabitMQ
        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CheckoutOrder(CheckoutOrderCommand dto)
        {
            var result = await _mediator.Send(dto);
            return Ok(result);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand dto)
        {
            await _mediator.Send(dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _mediator.Send(new DeleteOrderCommand { Id = id });
            return NoContent();
        }
    }
}
