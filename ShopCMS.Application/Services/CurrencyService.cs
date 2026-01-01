using ShopCMS.Domain.Entities;

public class CurrencyService
{
    private readonly IUnitOfWork _unitOfWork;

    public CurrencyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
}
