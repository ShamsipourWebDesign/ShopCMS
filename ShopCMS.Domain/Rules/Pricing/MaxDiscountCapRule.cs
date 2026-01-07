using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class MaxDiscountCapRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Max Discount Cap Rule";
        public string Description => "Ensures discount does not exceed the maximum allowed cap.";

        public PricingResult Evaluate(PricingContext context)
        {
            // If the total discount exceeds the max cap, set it to the cap
            if (context.TotalDiscount > context.MaxDiscountCap)
            {
                context.TotalDiscount = context.MaxDiscountCap;
            }

            // Calculate the final price after applying the capped discount
            var finalPrice = context.BasePrice - context.TotalDiscount;

            return new PricingResult
            {
                FinalPrice = finalPrice,
                AppliedRules = { $"Discount capped at {context.MaxDiscountCap} (Actual discount applied: {context.TotalDiscount})" }
            };
        }
    }
}
