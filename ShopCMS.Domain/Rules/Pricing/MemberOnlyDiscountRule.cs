using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class MemberOnlyDiscountRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Member Discount Rule";
        public string Description => "Applies discount only for members";

        public PricingResult Evaluate(PricingContext context)
        {
            // If not a member, return the base price without any discount
            if (!context.IsMember)
                return new PricingResult { FinalPrice = context.BasePrice };

            // Apply the member discount
            var final = context.BasePrice - context.MemberDiscount;

            // Ensure the final price doesn't go below zero
            if (final < 0) final = 0;

            return new PricingResult
            {
                FinalPrice = final,
                AppliedRules = { $"Member-only discount applied: {context.MemberDiscount}" }
            };
        }
    }
}
