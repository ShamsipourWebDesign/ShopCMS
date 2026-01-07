using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Application.Contracts;
using ShopCMS.Application.RulesEngine;
using ShopCMS.Application.Services.Composition;
using ShopCMS.Domain.Pricing;

namespace ShopCMS.Application.Services
{
    public class PricingService : IPricingService
    {
        private readonly RuleEngine<PricingContext, PricingResult> _ruleEngine;

        public PricingService()
        {
            // Build rule engine with default pricing rules
            _ruleEngine = new RuleEngine<PricingContext, PricingResult>(
                PricingRulesFactory.CreateDefaultRules()
            );
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
