using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Application.Contracts;
using ShopCMS.Application.RulesEngine;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Application.Services
{
    public class EligibilityService : IEligibilityService
    {
        private readonly RuleEngine<EligibilityContext, EligibilityResult> _ruleEngine;

        public EligibilityService(RuleEngine<EligibilityContext, EligibilityResult> ruleEngine)
        {
            _ruleEngine = ruleEngine;
        }

        public Task<EligibilityResult> CheckAsync(
            EligibilityContext context,
            CancellationToken cancellationToken = default)
        {
            var initial = new EligibilityResult
            {
                IsEligible = true
            };

            var result = _ruleEngine.Execute(context, initial);

            return Task.FromResult(result);
        }
    }
}
