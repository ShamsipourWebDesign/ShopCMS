// WebAPI/Controllers/CurrencyController.cs
using Microsoft.AspNetCore.Mvc;
using ShopCMS.Domain.Entities;
using Serilog;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ILogger<CurrencyController> _logger;
    private readonly CurrencyService _currencyService;

    public CurrencyController(CurrencyService currencyService, ILogger<CurrencyController> logger)
    {
        _currencyService = currencyService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddCurrency([FromBody] Currency currency)
    {
        await _currencyService.AddCurrencyAsync(currency);
        _logger.LogInformation("Received a request at {Time}", DateTime.UtcNow);
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
