using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Pricing;

namespace ShopCMS.Domain.Tests.Rules.Pricing
{
    public class MaxDiscountCapRuleTests
    {
        [Fact]
        public void Evaluate_ShouldCapDiscount_WhenTotalDiscountExceedsCap()
        {
            // Arrange: In this domain model, TotalDiscount is calculated by earlier rules.
            // MaxDiscountCapRule only caps the already-computed TotalDiscount.
            var rule = new MaxDiscountCapRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,

                // Simulate an already calculated discount from previous rules
                TotalDiscount = 600_000m,

                // Maximum allowed discount cap
                MaxDiscountCap = 300_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: discount must be capped to MaxDiscountCap
            ctx.TotalDiscount.Should().Be(300_000m);

            // Assert: final price should be base - capped discount
            result.FinalPrice.Should().Be(700_000m);

            // Assert: rule should provide an explanation message
            result.AppliedRules.Should().Contain(x => x.Contains("Discount capped at 300000"));
        }

        [Fact]
        public void Evaluate_ShouldNotChangeDiscount_WhenTotalDiscountIsWithinCap()
        {
            // Arrange
            var rule = new MaxDiscountCapRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                TotalDiscount = 200_000m,
                MaxDiscountCap = 300_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: no capping should happen
            ctx.TotalDiscount.Should().Be(200_000m);
            result.FinalPrice.Should().Be(800_000m);
        }
    }
}
