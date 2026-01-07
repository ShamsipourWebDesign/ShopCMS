using Microsoft.AspNetCore.Mvc;
using ShopCMS.Application.Contracts;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EligibilityController : ControllerBase
    {
        private readonly IEligibilityService _eligibilityService;

        public EligibilityController(IEligibilityService eligibilityService)
        {
            _eligibilityService = eligibilityService;
        }

        [HttpPost("check")]
        public async Task<ActionResult<EligibilityResult>> Check([FromBody] EligibilityContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

        var result = await _eligibilityService.CheckAsync(context);

        return Ok(result);
        }
    }
}
