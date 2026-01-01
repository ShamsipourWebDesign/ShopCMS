using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class MemberOnlyDiscountRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Member Discount Rule";
        public string Description => "Applies discount only for members";

        public PricingResult Evaluate(PricingContext context)
        {
            if (!context.IsMember)
                return new PricingResult { FinalPrice = context.BasePrice };

            var final = context.BasePrice - context.MemberDiscount;

            return new PricingResult
            {
                FinalPrice = final,
                AppliedRules = { "Member only discount applied" }
            };
        }
    }
}
