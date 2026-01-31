using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Pricing;

namespace ShopCMS.Domain.Tests.Pricing
{
    public class FixedDiscountRuleTests
    {
        [Fact]
        public void Evaluate_ShouldReturnBasePrice_WhenFixedDiscountIsZeroOrLess()
        {
            // Arrange: when FixedDiscount <= 0, rule should not change the price
            var rule = new FixedDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                FixedDiscount = 0m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: price remains the base price
            result.FinalPrice.Should().Be(1_000_000m);

            // Assert: rule provides an explainable trace message
            result.AppliedRules.Should().Contain("No discount applied (Fixed discount is 0 or less)");
        }

        [Fact]
        public void Evaluate_ShouldSubtractFixedDiscount_FromBasePrice_WhenFixedDiscountIsPositive()
        {
            // Arrange: when FixedDiscount > 0, rule subtracts it from BasePrice
            var rule = new FixedDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                FixedDiscount = 120_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: base - fixed discount
            result.FinalPrice.Should().Be(880_000m);

            // Assert: trace message includes the exact applied discount
            result.AppliedRules.Should().Contain("Fixed discount of 120000 applied");
        }

        [Fact]
        public void Evaluate_ShouldNotAllowFinalPrice_ToGoBelowZero()
        {
            // Arrange: if discount exceeds base price, final price must be clamped to 0
            var rule = new FixedDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 50_000m,
                FixedDiscount = 120_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: final price is clamped (never negative)
            result.FinalPrice.Should().Be(0m);

            // Assert: rule still records the applied discount trace
            result.AppliedRules.Should().Contain("Fixed discount of 120000 applied");
        }
    }
}
