using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class FixedDiscountRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Fixed Discount Rule";
        public string Description => "Subtracts a fixed discount amount from the base price.";

        public PricingResult Evaluate(PricingContext context)
        {
            // If no discount is applied or discount is less than or equal to 0, return base price
            if (context.FixedDiscount <= 0)
                return new PricingResult 
                { 
                    FinalPrice = context.BasePrice,
                    AppliedRules = { "No discount applied (Fixed discount is 0 or less)" }
                };

            var resultPrice = context.BasePrice - context.FixedDiscount;

            // Ensure price doesn't go below 0
            if (resultPrice < 0) resultPrice = 0;

            return new PricingResult
            {
                FinalPrice = resultPrice,
                AppliedRules = { $"Fixed discount of {context.FixedDiscount} applied" }
            };
        }
    }
}
