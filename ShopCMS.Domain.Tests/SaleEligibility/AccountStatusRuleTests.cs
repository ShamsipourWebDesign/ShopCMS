using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Eligibility;

namespace ShopCMS.Domain.Tests.Eligibility
{
    public class AccountStatusRuleTests
    {
        [Fact]
        public void Evaluate_ShouldBlockPurchase_WhenAccountIsBlocked()
        {
            // Arrange: blocked accounts must not be eligible to purchase
            var rule = new AccountStatusRule();

            var ctx = new PricingContext
            {
                IsAccountBlocked = true
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeFalse();
            result.BlockReason.Should().Be("User account is blocked");
        }

        [Fact]
        public void Evaluate_ShouldAllowPurchase_WhenAccountIsNotBlocked()
        {
            // Arrange
            var rule = new AccountStatusRule();

            var ctx = new PricingContext
            {
                IsAccountBlocked = false
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeTrue();
            result.BlockReason.Should().BeNull();
        }
    }
}
