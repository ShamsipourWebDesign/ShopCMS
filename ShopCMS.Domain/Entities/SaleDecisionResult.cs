using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{

    public class SaleDecisionResult
    {

        public int DecisionId { get; set; }

        public int RequestId { get; set; }

        public bool Allowed { get; set; }

        public string ReasonCode { get; set; }

        public decimal? FinalPrice { get; set; }

        public bool HasWarnings { get; set; }

        // Navigation back
        public SaleEligibilityRequest Request { get; set; }

        // Explainable Pricing → 1-n
        public ICollection<AppliedRule> AppliedRules { get; set; } = new List<AppliedRule>();
    }


}
