using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Application.Audit.RuleContract
{
    public interface IPricingRule
    {
        string Name { get; }
        PricingRuleResult Apply(decimal price);
    }

}
