using Microsoft.AspNetCore.Mvc;
using ShopCMS.Application.Contracts;
using ShopCMS.Domain.Pricing;

namespace ShopCMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricingController : ControllerBase
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        /// <summary>
        /// Calculates final price and returns explainable pricing output.
        /// </summary>
        [HttpPost("calculate")]
        public async Task<ActionResult<PricingResult>> Calculate([FromBody] PricingContext context)
        {
            if (context == null)
                return BadRequest("Pricing context is required.");

            var result = await _pricingService.CalculatePriceAsync(context);

            return Ok(result);
        }
    }
}
