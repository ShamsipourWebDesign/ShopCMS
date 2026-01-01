using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class PercentageDiscountRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Percentage Discount Rule";
        public string Description => "Applies percentage discount on base price";

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.PercentageDiscount <= 0)
                return new PricingResult { FinalPrice = context.BasePrice };

            var discount = context.BasePrice * (context.PercentageDiscount / 100m);

            return new PricingResult
            {
                FinalPrice = context.BasePrice - discount,
                AppliedRules = { $"Percentage discount {context.PercentageDiscount}% applied" }
            };
        }
    }
}
