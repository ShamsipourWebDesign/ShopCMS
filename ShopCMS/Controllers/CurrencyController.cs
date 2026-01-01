// WebAPI/Controllers/CurrencyController.cs
using Microsoft.AspNetCore.Mvc;
using ShopCMS.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly CurrencyService _currencyService;

    public CurrencyController(CurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCurrency([FromBody] Currency currency)
    {
        await _currencyService.AddCurrencyAsync(currency);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCurrencyById(int id)
    {
        var currency = await _currencyService.GetCurrencyByIdAsync(id);
        if (currency == null)
            return NotFound();
        return Ok(currency);
    }
}
