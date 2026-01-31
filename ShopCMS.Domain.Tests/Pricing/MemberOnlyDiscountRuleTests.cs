using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Pricing;

namespace ShopCMS.Domain.Tests.Pricing
{
    public class MemberOnlyDiscountRuleTests
    {
        [Fact]
        public void Evaluate_ShouldReturnBasePrice_WhenUserIsNotMember()
        {
            // Arrange: if user is not a member, rule should not apply any member discount
            var rule = new MemberOnlyDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                IsMember = false,
                MemberDiscount = 100_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: final price stays base price
            result.FinalPrice.Should().Be(1_000_000m);

            // Assert: code returns PricingResult without adding any AppliedRules message
            result.AppliedRules.Should().BeEmpty();
        }

        [Fact]
        public void Evaluate_ShouldApplyMemberDiscount_WhenUserIsMember()
        {
            // Arrange: if user is a member, rule subtracts MemberDiscount from BasePrice
            var rule = new MemberOnlyDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                IsMember = true,
                MemberDiscount = 100_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.FinalPrice.Should().Be(900_000m);

            // Assert: explainability message should include the discount amount
            result.AppliedRules.Should().Contain("Member-only discount applied: 100000");
        }

        [Fact]
        public void Evaluate_ShouldNotAllowFinalPrice_ToGoBelowZero()
        {
            // Arrange: if member discount exceeds base price, final price must be clamped to 0
            var rule = new MemberOnlyDiscountRule();

            var ctx = new PricingContext
            {
                BasePrice = 50_000m,
                IsMember = true,
                MemberDiscount = 120_000m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.FinalPrice.Should().Be(0m);
            result.AppliedRules.Should().Contain("Member-only discount applied: 120000");
        }
    }
}
