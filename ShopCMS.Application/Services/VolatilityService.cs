using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Application.Contracts;
using ShopCMS.Application.RulesEngine;
using ShopCMS.Application.Services.Composition;
using ShopCMS.Domain.Volatility;

namespace ShopCMS.Application.Services
{
    public class VolatilityService : IVolatilityService
    {
        private readonly RuleEngine<VolatilityContext, VolatilityResult> _ruleEngine;

        public VolatilityService()
        {
            _ruleEngine = new RuleEngine<VolatilityContext, VolatilityResult>(
                VolatilityRulesFactory.CreateDefaultRules()
            );
        }

        public Task<VolatilityResult> CheckAsync(
            VolatilityContext context,
            CancellationToken cancellationToken = default)
        {
            var result = _ruleEngine.Execute(context, new VolatilityResult());

            return Task.FromResult(result);
        }
    }
}
