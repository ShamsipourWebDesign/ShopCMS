using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class CouponDiscountRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Coupon Discount Rule";
        public string Description => "Applies discount when coupon code exists";

        public PricingResult Evaluate(PricingContext context)
        {
            if (!context.HasCoupon)
                return new PricingResult { FinalPrice = context.BasePrice };

            return new PricingResult
            {
                FinalPrice = context.BasePrice - context.CouponAmount,
                AppliedRules = { $"Coupon discount {context.CouponAmount} applied" }
            };
        }
    }
}
