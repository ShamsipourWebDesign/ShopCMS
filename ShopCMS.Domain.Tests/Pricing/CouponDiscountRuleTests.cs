using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Pricing;

namespace ShopCMS.Domain.Tests.Pricing
{
    public class CouponDiscountRuleTests
    {
        [Fact]
        public void Evaluate_ShouldReturnBasePrice_AndAddMessage_WhenHasCouponIsFalse()
        {
            // Arrange: when HasCoupon is false, rule should not apply any discount
            var rule = new CouponDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                HasCoupon = false,
                CouponAmount = 50_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: price remains base price
            result.FinalPrice.Should().Be(1_000_000m);

            // Assert: rule adds a clear trace entry
            result.AppliedRules.Should().Contain("No coupon applied");
        }

        [Fact]
        public void Evaluate_ShouldSubtractCouponAmount_WhenHasCouponIsTrue()
        {
            // Arrange: when coupon exists, rule subtracts CouponAmount from BasePrice
            var rule = new CouponDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                HasCoupon = true,
                CouponAmount = 50_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.FinalPrice.Should().Be(950_000m);

            // Assert: trace message should include the exact coupon amount
            result.AppliedRules.Should().Contain("Coupon discount of 50000 applied");
        }
    }
}
