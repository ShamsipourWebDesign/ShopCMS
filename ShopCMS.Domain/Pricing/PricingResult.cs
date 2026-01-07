using System.Collections.Generic;

namespace ShopCMS.Domain.Pricing
{
    public class PricingResult
    {
        // Final calculated price after all rules
        public decimal FinalPrice { get; set; }

        // List of applied rule names (for explainable output)
        public List<string> AppliedRules { get; set; } = new();

        // Whether the pricing is eligible (can be blocked by some rules)
        public bool IsEligible { get; set; } = true;

        // If blocked, this will contain the reason
        public string? BlockReason { get; set; }
    }
}
