using Microsoft.AspNetCore.Mvc;
using ShopCMS.Contracts.AuditPricing;
using ShopCMS.Application.Audit.UseCase;

namespace ShopCMS.Controllers
{
   
   

    [ApiController]
    [Route("api/[controller]")]
    public class PricingController : ControllerBase
    {
        private readonly PricingService _pricingService;

        public PricingController(PricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculatePrice(PriceRequest request)
        {
            var price = await _pricingService.CalculatePriceAsync(request.BasePrice);
            return Ok(price);
        }
    }

}
