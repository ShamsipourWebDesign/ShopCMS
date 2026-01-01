using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace SalesDecisionEngine.Domain.Rules.Inventory
{
    public class PerOrderQuantityLimitRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Per Order Quantity Limit Rule";
        public string Description => "Restricts maximum quantity per order";

        public int MaxPerOrder { get; }

        public PerOrderQuantityLimitRule(int max)
        {
            MaxPerOrder = max;
        }

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.Quantity > MaxPerOrder)
            {
                return new PricingResult
                {
                    IsEligible = false,
                    BlockReason = $"Cannot purchase more than {MaxPerOrder} per order"
                };
            }

            return new PricingResult { IsEligible = true };
        }
    }
}
