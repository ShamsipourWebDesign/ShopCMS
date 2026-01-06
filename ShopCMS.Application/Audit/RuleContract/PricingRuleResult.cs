using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCMS.Infrastructure.Audit;

namespace ShopCMS.Application.Audit.RuleContract
{
    
    public record PricingRuleResult(
     bool Applied,
     decimal OutputPrice,
     string Reason
 );

}
