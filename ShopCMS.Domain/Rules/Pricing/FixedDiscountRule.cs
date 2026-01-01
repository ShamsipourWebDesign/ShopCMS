using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class FixedDiscountRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Fixed Discount Rule";
        public string Description => "Subtracts a fixed discount amount";

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.FixedDiscount <= 0)
                return new PricingResult { FinalPrice = context.BasePrice };

            var resultPrice = context.BasePrice - context.FixedDiscount;

            if (resultPrice < 0) resultPrice = 0;

            return new PricingResult
            {
                FinalPrice = resultPrice,
                AppliedRules = { $"Fixed discount {context.FixedDiscount} applied" }
            };
        }
    }
}
