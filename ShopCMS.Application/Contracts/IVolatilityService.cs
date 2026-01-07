using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Domain.Volatility;

namespace ShopCMS.Application.Contracts
{
    public interface IVolatilityService
    {
        Task<VolatilityResult> CheckAsync(
            VolatilityContext context,
            CancellationToken cancellationToken = default);
    }
}
