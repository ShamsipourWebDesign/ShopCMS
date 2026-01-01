using System.Collections.Generic;

namespace ShopCMS.Domain.Pricing
{
    public class PricingResult
    {
        public decimal FinalPrice { get; set; }

        public List<string> AppliedRules { get; set; } = new();

        public bool IsEligible { get; set; } = true;

        public string? BlockReason { get; set; }
    }
}
