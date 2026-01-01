using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace SalesDecisionEngine.Domain.Rules.Inventory
{
    public class MaxPerUserRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Max Per User Rule";
        public string Description => "Limits maximum quantity per user";

        public int MaxPerUser { get; }

        public MaxPerUserRule(int max)
        {
            MaxPerUser = max;
        }

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.Quantity > MaxPerUser)
            {
                return new PricingResult
                {
                    IsEligible = false,
                    BlockReason = $"User cannot purchase more than {MaxPerUser} units"
                };
            }

            return new PricingResult { IsEligible = true };
        }
    }
}
