using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        private readonly ILogger<DiscountController> _logger;

        public DiscountController(IDiscountRepository discountRepository, ILogger<DiscountController> _logger)
        {
            _discountRepository = discountRepository;
            this._logger = _logger;
        }

        [HttpGet("{productName}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Coupon))]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDisount(productName);

            return Ok(coupon);
        }
        
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Coupon))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            var result = await _discountRepository.CreateDiscount(coupon);

            if (!result)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }

            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName });
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Coupon))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            var result = await _discountRepository.UpdateDiscount(coupon);

            if (!result)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }

            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName });
        }

        [HttpDelete("{productName}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            var result = await _discountRepository.DeleteDiscount(productName);

            return Ok(result);
        }
    }
}
