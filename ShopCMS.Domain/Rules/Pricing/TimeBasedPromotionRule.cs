using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class TimeBasedPromotionRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Time Promotion Rule";
        public string Description => "Applies discount only in specific time range";

        public PricingResult Evaluate(PricingContext context)
        {
            if (DateTime.Now < context.PromoStart || DateTime.Now > context.PromoEnd)
                return new PricingResult { FinalPrice = context.BasePrice };

            return new PricingResult
            {
                FinalPrice = context.BasePrice - context.PromoDiscount,
                AppliedRules = { "Time-based promotion applied" }
            };
        }
    }
}
