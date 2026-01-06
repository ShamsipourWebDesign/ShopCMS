using ShopCMS.Application.Audit.RuleContract;
using ShopCMS.Domain.PricingAudit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCMS.Infrastructure.Audit;

namespace ShopCMS.Application.Audit.UseCase
{
    public class PricingService
    {
        private readonly IEnumerable<IPricingRule> _rules;
        private readonly IPricingAuditRepository _auditRepository;

        public PricingService(
            IEnumerable<IPricingRule> rules,
            IPricingAuditRepository auditRepository)
        {
            _rules = rules;
            _auditRepository = auditRepository;
        }

        public async Task<decimal> CalculatePriceAsync(decimal basePrice)
        {
            var audit = new PricingAudit(basePrice);
            var currentPrice = basePrice;

            foreach (var rule in _rules)
            {
                var result = rule.Apply(currentPrice);

                var ruleAudit = new PricingRuleAudit(
                    rule.Name,
                    result.Applied,
                    currentPrice,
                    result.OutputPrice,
                    result.Reason
                );

                audit.AddRuleAudit(ruleAudit);
                currentPrice = result.OutputPrice;
            }

            await _auditRepository.SaveAsync(audit, CancellationToken.None);

            return currentPrice;
        }
    }

}
