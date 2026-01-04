using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Application.Contracts
{
    public interface IEligibilityService
    {
        Task<EligibilityResult> CheckAsync(
            EligibilityContext context,
            CancellationToken cancellationToken = default);
    }
}
