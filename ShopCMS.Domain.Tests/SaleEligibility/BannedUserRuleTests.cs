using FluentAssertions;
using Xunit;

using ShopCMS.Domain.SaleEligibility;
using ShopCMS.Domain.SaleEligibility.Rules;

namespace ShopCMS.Domain.Tests.SaleEligibility
{
    public class BannedUserRuleTests
    {
        [Fact]
        public void Evaluate_ShouldBlock_WhenUserIsBanned()
        {
            // Arrange: banned user must not be eligible
            var rule = new BannedUserRule();

            var ctx = new EligibilityContext
            {
                IsBanned = true
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeFalse();
            result.Reason.Should().Be("User account is banned");
        }

        [Fact]
        public void Evaluate_ShouldAllow_WhenUserIsNotBanned()
        {
            // Arrange
            var rule = new BannedUserRule();

            var ctx = new EligibilityContext
            {
                IsBanned = false
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeTrue();
            result.Reason.Should().BeNull();
        }
    }
}
