using System.Collections.Generic;
using ShopCMS.Domain.Rules;
using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Pricing;

namespace ShopCMS.Application.Services.Composition
{
    public static class PricingRulesFactory
    {
        public static List<IRule<PricingContext, PricingResult>> CreateDefaultRules()
        {
            return new List<IRule<PricingContext, PricingResult>>
            {
                new BasePriceRule(),
                new PercentageDiscountRule(),
                new FixedDiscountRule(),
                new MemberOnlyDiscountRule(),
                new CouponDiscountRule(),
                new MaxDiscountCapRule(),
                new TimeBasedPromotionRule(),
                new CurrencyConversionRule()
            };
        }
    }
}
