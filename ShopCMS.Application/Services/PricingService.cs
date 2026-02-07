using ShopCMS.Application.Contracts;
using ShopCMS.Application.RulesEngine;
using ShopCMS.Application.Services.Composition;
using ShopCMS.Domain.Interfaces;
using ShopCMS.Domain.Pricing;
using ShopCMS.Infrastructure.External;
using System.Threading;
using System.Threading.Tasks;

namespace ShopCMS.Application.Services
{
    public class PricingService : IPricingService
    {
        

        private readonly Contracts.ICurrencyRateProvider _currencyRateProvider;

        private readonly RuleEngine<PricingContext, PricingResult> _ruleEngine;

        public PricingService(ICurrencyRateProvider @object)
        {

             
            _currencyRateProvider = currencyRateProvider;
            

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

            if (context is ICurrencyAwarePricingContext currencyContext &&
                !string.IsNullOrWhiteSpace(currencyContext.TargetCurrency))
            {
                var rate = _currencyRateProvider
                    .GetRateAsync(
                        "USD",
                        currencyContext.TargetCurrency,
                        cancellationToken)
                    .GetAwaiter()
                    .GetResult();

                result.FinalPrice *= rate;
            }

            return Task.FromResult(result);
        }

    }
    
}
