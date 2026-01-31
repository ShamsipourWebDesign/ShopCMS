using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Eligibility;

namespace ShopCMS.Domain.Tests.Eligibility
{
    public class UserMustBeLoggedInRuleTests
    {
        [Fact]
        public void Evaluate_ShouldBlockPurchase_WhenUserIsNotLoggedIn()
        {
            // Arrange: user must be logged in to proceed
            var rule = new UserMustBeLoggedInRule();

            var ctx = new PricingContext
            {
                IsLoggedIn = false
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeFalse();
            result.BlockReason.Should().Be("User must be logged in to purchase");
        }

        [Fact]
        public void Evaluate_ShouldAllowPurchase_WhenUserIsLoggedIn()
        {
            // Arrange
            var rule = new UserMustBeLoggedInRule();

            var ctx = new PricingContext
            {
                IsLoggedIn = true
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeTrue();
            result.BlockReason.Should().BeNull();
        }
    }
}
