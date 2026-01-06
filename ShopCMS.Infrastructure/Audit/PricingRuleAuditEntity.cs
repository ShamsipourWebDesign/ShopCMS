using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Infrastructure.Audit
{
    public class PricingRuleAuditEntity
    {
        public Guid Id { get; set; }
        public Guid PricingAuditId { get; set; }
        public string RuleName { get; set; }
        public bool Applied { get; set; }
        public decimal InputPrice { get; set; }
        public decimal OutputPrice { get; set; }
        public string Reason { get; set; }
    }

}
