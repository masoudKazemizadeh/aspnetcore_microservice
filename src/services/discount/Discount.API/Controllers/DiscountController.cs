using Discount.API.Attributes;
using Discount.API.Entity;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/Discount")]
    [ApiVersion("v1.0")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{productName}",Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            return Ok(await _discountRepository.GetDiscountAsync(productName));
        }
        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            var result = await _discountRepository.DeleteDiscountAsync(productName);
            if (!result)
                throw new Exception("it didn't delete");
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateDiscount(Coupon dto)
        {
            if (dto == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var result = await _discountRepository.CreateDiscountAsync(dto);

            if (!result)
                throw new Exception("it didn't create");

            return CreatedAtRoute("GetDiscount", new { dto.ProductName }, dto);


        }

        [HttpPut]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateDiscount(Coupon dto)
        {
            if (dto == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var result = await _discountRepository.UpdateDiscountAsync(dto);
            if (!result)
                throw new Exception("it didn't update");
            return NoContent();
        }

    }
}
