
using Microsoft.AspNetCore.Mvc;
using ShopCMS.Application.Services;
using ShopCMS.Domain.Entities.PriceLockApi;
using ShopCMS.DTOs;

[ApiController]
[Route("api/price-lock")]
public class PriceLockApiController : ControllerBase
{
    private readonly PriceLockService _priceLockService;

    public PriceLockApiController(PriceLockService PriceLockService)
    {
        _priceLockService = PriceLockService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePriceLockApiRequest request)
    {
        var id = await _priceLockService.CreateAsync(
            request.ProductId,
            request.UserId,
            request.FinalPrice,
            TimeSpan.FromMinutes(10));

        return Ok(id);
    }
}
