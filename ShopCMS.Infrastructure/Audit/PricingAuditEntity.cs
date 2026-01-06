using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Infrastructure.Audit
{
    public class PricingAuditEntity
    {
        public Guid Id { get; set; }
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<PricingRuleAuditEntity> RuleAudits { get; set; }
    }

}
