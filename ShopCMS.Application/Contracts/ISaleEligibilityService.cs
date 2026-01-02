using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Domain.Pricing;

namespace ShopCMS.Application.Contracts
{
    public interface ISaleEligibilityService
    {
        Task<bool> IsEligibleAsync(
            PricingContext context,
            CancellationToken cancellationToken = default);

        Task<string?> GetBlockReasonAsync(
            PricingContext context,
            CancellationToken cancellationToken = default);
    }
}
