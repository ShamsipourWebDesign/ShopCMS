using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Domain.Pricing;

namespace ShopCMS.Application.Contracts
{
    public interface IAuditLogger
    {
        Task LogPricingDecisionAsync(
            PricingContext context,
            PricingResult result,
            CancellationToken cancellationToken = default);
    }
}
