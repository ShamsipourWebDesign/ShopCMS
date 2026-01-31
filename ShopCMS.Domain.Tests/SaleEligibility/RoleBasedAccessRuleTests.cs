using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Eligibility;

namespace ShopCMS.Domain.Tests.Eligibility
{
    public class RoleBasedAccessRuleTests
    {
        [Fact]
        public void Evaluate_ShouldBlockPurchase_WhenUserRoleIsNotAllowed()
        {
            // Arrange: only specific roles are allowed to buy this product
            var rule = new RoleBasedAccessRule("Admin", "Seller");

            var ctx = new PricingContext
            {
                UserRole = "Consumer"
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeFalse();
            result.BlockReason.Should().Be("User role is not allowed to buy this product");
        }

        [Fact]
        public void Evaluate_ShouldAllowPurchase_WhenUserRoleIsAllowed()
        {
            // Arrange
            var rule = new RoleBasedAccessRule("Admin", "Seller");

            var ctx = new PricingContext
            {
                UserRole = "Seller"
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeTrue();
            result.BlockReason.Should().BeNull();
        }
    }
}
