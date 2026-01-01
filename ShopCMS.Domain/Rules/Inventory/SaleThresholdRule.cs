using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace SalesDecisionEngine.Domain.Rules.Inventory
{
    public class SaleThresholdRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Sale Threshold Rule";
        public string Description => "Blocks sale when stock is below threshold";

        public int Threshold { get; }

        public SaleThresholdRule(int threshold)
        {
            Threshold = threshold;
        }

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.Quantity < Threshold)
            {
                return new PricingResult
                {
                    IsEligible = false,
                    BlockReason = $"Stock below allowed threshold ({Threshold})"
                };
            }

            return new PricingResult { IsEligible = true };
        }
    }
}
