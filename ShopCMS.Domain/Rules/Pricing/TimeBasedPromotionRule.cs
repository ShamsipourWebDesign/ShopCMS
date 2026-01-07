using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class TimeBasedPromotionRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Time Promotion Rule";
        public string Description => "Applies discount only in specific time range";

        public PricingResult Evaluate(PricingContext context)
        {
            // Ensure promo dates are valid
            if (context.PromoStart == null || context.PromoEnd == null || context.PromoStart > context.PromoEnd)
            {
                return new PricingResult { FinalPrice = context.BasePrice }; // Invalid date range, no discount
            }

            // If current time is outside the promotion period
            if (DateTime.Now < context.PromoStart || DateTime.Now > context.PromoEnd)
                return new PricingResult { FinalPrice = context.BasePrice };

            // Apply the discount within the valid promotion period
            var finalPrice = context.BasePrice - context.PromoDiscount;

            // Ensure the final price doesn't go below zero
            if (finalPrice < 0) finalPrice = 0;

            // Return the result with the final price and applied rules
            return new PricingResult
            {
                FinalPrice = finalPrice,
                AppliedRules = { "Time-based promotion applied" }
            };
        }
    }
}
