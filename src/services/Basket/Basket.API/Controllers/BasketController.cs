using Basket.API.Entity;
using Basket.API.Repositories;
using Basket.API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/Basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;
        

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        [HttpGet("{username}",Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string username)
        {
            return Ok((await _basketService.GetBasketAsync(username) ?? new ShoppingCart(username)));
        }
        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _basketService.DeleteBasketAsync(username);
            return NoContent();
        }

        [HttpPut(Name = "UpdateBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateBasket(ShoppingCart model)
        {
            return Ok(await _basketService.UpdateBasketAsync(model));
        }

        [HttpPost("checkout",Name = "Checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckoutBasket(BasketCheckout dto)
        {
            var result = await _basketService.CheckoutAsync(dto);
            return result ? Accepted() : BadRequest();

        }

    }
}
