using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class MaxDiscountCapRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Max Discount Cap Rule";
        public string Description => "Ensures discount does not exceed max cap";

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.TotalDiscount > context.MaxDiscountCap)
                context.TotalDiscount = context.MaxDiscountCap;

            return new PricingResult
            {
                FinalPrice = context.BasePrice - context.TotalDiscount,
                AppliedRules = { $"Discount capped at {context.MaxDiscountCap}" }
            };
        }
    }
}
