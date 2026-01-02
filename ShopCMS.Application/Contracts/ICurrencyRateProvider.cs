using System.Threading;
using System.Threading.Tasks;

namespace ShopCMS.Application.Contracts
{
    public interface ICurrencyRateProvider
    {
        Task<decimal> GetRateAsync(
            string baseCurrency,
            string quoteCurrency,
            CancellationToken cancellationToken = default);
    }
}
