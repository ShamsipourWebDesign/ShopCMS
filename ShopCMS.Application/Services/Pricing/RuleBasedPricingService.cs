using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Application.Contracts;
using ShopCMS.Application.RulesEngine;
using ShopCMS.Application.Services.Composition;
using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Application.Services
{
    public class RuleBasedPricingService : IPricingService
    {
        public Task<PricingResult> CalculatePriceAsync(
            PricingContext context,
            CancellationToken cancellationToken = default)
        {
            var rules = PricingRulesFactory.CreateDefaultRules();

            var engine = new RuleEngine<PricingContext, PricingResult>(rules);

            var initial = new PricingResult
            {
                FinalPrice = context.BasePrice
            };

            var result = engine.Execute(context, initial);

            return Task.FromResult(result);
        }
    }
}
