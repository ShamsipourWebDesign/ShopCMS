using Microsoft.AspNetCore.Mvc;
using ShopCMS.Application.Contracts;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EligibilityController : ControllerBase
    {
        private readonly ILogger<EligibilityController> _logger;
        private readonly IEligibilityService _eligibilityService;

        public EligibilityController(IEligibilityService eligibilityService, ILogger<EligibilityController> logger)
        {
            _eligibilityService = eligibilityService;
            _logger = logger;
        }

        [HttpPost("check")]
        public async Task<IActionResult> Check([FromBody] EligibilityContext context)
        {
            var result = await _eligibilityService.CheckAsync(context);
            _logger.LogInformation("Received a request at {Time}", DateTime.UtcNow);
            return Ok(result);
        }
    }
}
