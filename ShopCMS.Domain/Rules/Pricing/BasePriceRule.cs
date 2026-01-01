using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class BasePriceRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Base Price Rule";
        public string Description => "Applies the base price of the product";

        public PricingResult Evaluate(PricingContext context)
        {
            return new PricingResult
            {
                FinalPrice = context.BasePrice,
                AppliedRules = { "Base price applied" }
            };
        }
    }
}
