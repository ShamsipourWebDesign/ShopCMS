using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Pricing;

namespace ShopCMS.Domain.Tests.Pricing
{
    public class PricingRulesCombinationTests
    {
        [Fact]
        public void PricingRules_ShouldProduceExpectedFinalPrice_WhenMultipleRulesAreApplied()
        {
            // Arrange: simulate a realistic pricing scenario
            var basePrice = 1_000_000m;

            var context = new PricingContext
            {
                BasePrice = basePrice,
                PercentageDiscount = 10m,   // 100,000
                FixedDiscount = 50_000m,    // handled by FixedDiscountRule
                HasCoupon = true,
                CouponAmount = 30_000m,
                IsMember = true,
                MemberDiscount = 20_000m
            };

            // Apply rules sequentially (as they would be in the real pipeline)
            var percentageRule = new PercentageDiscountRule();
            var fixedRule = new FixedDiscountRule();
            var couponRule = new CouponDiscountRule();
            var memberRule = new MemberOnlyDiscountRule();

            var r1 = percentageRule.Evaluate(context);
            context.BasePrice = r1.FinalPrice;

            var r2 = fixedRule.Evaluate(context);
            context.BasePrice = r2.FinalPrice;

            var r3 = couponRule.Evaluate(context);
            context.BasePrice = r3.FinalPrice;

            var r4 = memberRule.Evaluate(context);

            // Act
            var finalPrice = r4.FinalPrice;

            // Assert: final price should be deterministic and correct
            finalPrice.Should().Be(800_000m);

            // Assert: each rule contributed to explainability
            r1.AppliedRules.Should().Contain("Percentage discount 10% applied");
            r2.AppliedRules.Should().Contain("Fixed discount of 50000 applied");
            r3.AppliedRules.Should().Contain("Coupon discount of 30000 applied");
            r4.AppliedRules.Should().Contain("Member-only discount applied: 20000");
        }
    }
}
