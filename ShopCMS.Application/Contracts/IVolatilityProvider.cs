using System.Threading.Tasks;

namespace ShopCMS.Application.Contracts
{
    public interface IVolatilityProvider
    {
        Task<decimal> GetCurrentPriceAsync(string symbol);
    }
}
