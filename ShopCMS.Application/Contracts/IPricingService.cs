using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Domain.Pricing;

namespace ShopCMS.Application.Contracts
{
    public interface IPricingService
    {
        Task<PricingResult> CalculatePriceAsync(
            PricingContext context,
            CancellationToken cancellationToken = default);
    }
}
