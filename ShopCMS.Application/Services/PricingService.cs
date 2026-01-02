using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Application.Contracts;
using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Application.Services
{
    public class PricingService : IPricingService
    {
        private readonly IRuleEngine<PricingContext, PricingResult> _ruleEngine;

        public PricingService(IRuleEngine<PricingContext, PricingResult> ruleEngine)
        {
            _ruleEngine = ruleEngine;
        }

        public Task<PricingResult> CalculatePriceAsync(
            PricingContext context,
            CancellationToken cancellationToken = default)
        {
            var initial = new PricingResult
            {
                FinalPrice = context.BasePrice
            };

            var result = _ruleEngine.Execute(context, initial);

            return Task.FromResult(result);
        }
    }
}
