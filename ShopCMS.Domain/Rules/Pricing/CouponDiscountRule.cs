using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class CouponDiscountRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Coupon Discount Rule";
        public string Description => "Applies discount when coupon code is provided and valid.";

        public PricingResult Evaluate(PricingContext context)
        {
            if (!context.HasCoupon)
                return new PricingResult 
                { 
                    FinalPrice = context.BasePrice,
                    AppliedRules = { "No coupon applied" } 
                };

            return new PricingResult
            {
                FinalPrice = context.BasePrice - context.CouponAmount,
                AppliedRules = { $"Coupon discount of {context.CouponAmount} applied" }
            };
        }
    }
}
