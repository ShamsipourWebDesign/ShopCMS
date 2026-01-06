using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.PricingAudit
{
    public class PricingRuleAudit
    {
        public string RuleName { get; }
        public bool Applied { get; }
        public decimal InputPrice { get; }
        public decimal OutputPrice { get; }
        public string Reason { get; }

        public PricingRuleAudit(
            string ruleName,
            bool applied,
            decimal inputPrice,
            decimal outputPrice,
            string reason)
        {
            RuleName = ruleName;
            Applied = applied;
            InputPrice = inputPrice;
            OutputPrice = outputPrice;
            Reason = reason;
        }
    }

}
