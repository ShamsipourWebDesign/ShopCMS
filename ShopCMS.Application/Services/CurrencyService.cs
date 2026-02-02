using ShopCMS.Domain.Entities;
using ShopCMS.Domain.Interfaces;

public class CurrencyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExchangeRateClient _exchangeRateClient;


    public CurrencyService(IUnitOfWork unitOfWork , IExchangeRateClient exchangeRateClient)
    {
        _unitOfWork = unitOfWork;
        _exchangeRateClient = exchangeRateClient;
    }

    public async Task AddCurrencyAsync(Currency currency)
    {
        await _unitOfWork.Repository<Currency>().AddAsync(currency);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<Currency> GetCurrencyByIdAsync(int id)
    {
        return await _unitOfWork.Repository<Currency>().GetByIdAsync(id);
    }


    public async Task<decimal> ConvertAmountAsync(
       decimal amount,
       string fromCurrency,
       string toCurrency)
    {
        var rate = await _exchangeRateClient
            .GetExchangeRateAsync(fromCurrency, toCurrency);

        return amount * rate;
    }
}
