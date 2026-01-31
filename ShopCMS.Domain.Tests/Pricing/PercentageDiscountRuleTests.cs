using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Pricing;

namespace ShopCMS.Domain.Tests.Pricing
{
    public class PercentageDiscountRuleTests
    {
        [Fact]
        public void Evaluate_ShouldReturnBasePrice_WhenPercentageDiscountIsZeroOrLess()
        {
            // Arrange: when PercentageDiscount <= 0, rule should not apply any discount
            var rule = new PercentageDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                PercentageDiscount = 0m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: final price stays equal to base price
            result.FinalPrice.Should().Be(1_000_000m);

            // Assert: code returns a PricingResult without adding any AppliedRules message
            result.AppliedRules.Should().BeEmpty();
        }

        [Fact]
        public void Evaluate_ShouldApplyPercentageDiscount_OnBasePrice()
        {
            // Arrange: for positive discount, rule reduces price by (BasePrice * Percentage / 100)
            var rule = new PercentageDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                PercentageDiscount = 10m // 10% => 100,000 discount
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.FinalPrice.Should().Be(900_000m);

            // Assert: rule must provide an explainability message
            result.AppliedRules.Should().Contain("Percentage discount 10% applied");
        }

        [Fact]
        public void Evaluate_ShouldNotAllowFinalPrice_ToGoBelowZero()
        {
            // Arrange: if discount becomes greater than base price, final price is clamped to 0
            var rule = new PercentageDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 100m,
                PercentageDiscount = 200m // 200% => final price would be -100 => must clamp to 0
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: final price should never be negative
            result.FinalPrice.Should().Be(0m);

            // Assert: message still exists because discount was applied
            result.AppliedRules.Should().Contain("Percentage discount 200% applied");
        }
    }
}
