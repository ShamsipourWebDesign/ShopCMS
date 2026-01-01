using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
        public class AppliedRule
        {
        
        public int AppliedRuleId { get; set; }

            public int DecisionId { get; set; }

            public string RuleName { get; set; }

            public string RuleType { get; set; }

            public int RuleVersion { get; set; }

            public string EffectDescription { get; set; }

            // Navigation
            public SaleDecisionResult Decision { get; set; }
        }
    

}
