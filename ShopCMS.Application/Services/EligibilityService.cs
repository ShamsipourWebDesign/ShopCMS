using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Application.Contracts;
using ShopCMS.Application.RulesEngine;
using ShopCMS.Application.Services.Composition;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Application.Services
{
    public class EligibilityService : IEligibilityService
    {
        private readonly RuleEngine<EligibilityContext, EligibilityResult> _engine;

        public EligibilityService()
        {
            // Build default rule pipeline
            _engine = new RuleEngine<EligibilityContext, EligibilityResult>(
                EligibilityRulesFactory.CreateDefaultRules()
            );
        }

        public Task<EligibilityResult> CheckAsync(
            EligibilityContext context,
            CancellationToken cancellationToken = default)
        {
            // initial assumption
            var initial = new EligibilityResult
            {
                IsEligible = true
            };

            // run pipeline
            var result = _engine.Execute(context, initial);

            return Task.FromResult(result);
        }
    }
}
