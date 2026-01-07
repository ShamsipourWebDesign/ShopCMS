using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class PercentageDiscountRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Percentage Discount Rule";
        public string Description => "Applies percentage discount on base price";

        public PricingResult Evaluate(PricingContext context)
        {
            // If the discount is 0 or less, no discount applied
            if (context.PercentageDiscount <= 0)
                return new PricingResult { FinalPrice = context.BasePrice };

            // Calculate the discount amount
            var discount = context.BasePrice * (context.PercentageDiscount / 100m);

            // Ensure the final price does not go below zero
            var finalPrice = context.BasePrice - discount;
            if (finalPrice < 0) finalPrice = 0;

            // Return the result with the final price and applied rules
            return new PricingResult
            {
                FinalPrice = finalPrice,
                AppliedRules = { $"Percentage discount {context.PercentageDiscount}% applied" }
            };
        }
    }
}
