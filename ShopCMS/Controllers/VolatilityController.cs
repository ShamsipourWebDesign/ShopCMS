using Microsoft.AspNetCore.Mvc;
using ShopCMS.Application.Contracts;
using ShopCMS.Domain.Volatility;

namespace ShopCMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolatilityController : ControllerBase
    {
        private readonly IVolatilityService _volatilityService;

        public VolatilityController(IVolatilityService volatilityService)
        {
            _volatilityService = volatilityService;
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckVolatility([FromBody] VolatilityContext context)
        {
            if (context == null)
                return BadRequest("Context is required");

            var result = await _volatilityService.CheckAsync(context);

            return Ok(result);
        }
    }
}
