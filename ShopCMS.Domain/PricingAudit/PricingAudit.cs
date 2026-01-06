using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.PricingAudit
{
    public class PricingAudit
    {
        public Guid Id { get; private set; }
        public decimal BasePrice { get; private set; }
        public decimal FinalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<PricingRuleAudit> _ruleAudits = new();
        public IReadOnlyCollection<PricingRuleAudit> RuleAudits => _ruleAudits;

        public PricingAudit(decimal basePrice)
        {
            Id = Guid.NewGuid();
            BasePrice = basePrice;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddRuleAudit(PricingRuleAudit audit)
        {
            _ruleAudits.Add(audit);
            FinalPrice = audit.OutputPrice;
        }
    }

}
