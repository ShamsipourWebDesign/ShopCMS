using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Eligibility;

namespace ShopCMS.Domain.Tests.Eligibility
{
    public class MemberOnlyProductRuleTests
    {
        [Fact]
        public void Evaluate_ShouldBlockPurchase_WhenProductIsMemberOnly_AndUserIsNotMember()
        {
            // Arrange: member-only product should be blocked for non-members
            var rule = new MemberOnlyProductRule(isMemberOnly: true);

            var ctx = new PricingContext
            {
                IsMember = false
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeFalse();
            result.BlockReason.Should().Be("This product is only available for members");
        }

        [Fact]
        public void Evaluate_ShouldAllowPurchase_WhenProductIsMemberOnly_AndUserIsMember()
        {
            // Arrange
            var rule = new MemberOnlyProductRule(isMemberOnly: true);

            var ctx = new PricingContext
            {
                IsMember = true
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeTrue();
            result.BlockReason.Should().BeNull();
        }

        [Fact]
        public void Evaluate_ShouldAllowPurchase_WhenProductIsNotMemberOnly_EvenIfUserIsNotMember()
        {
            // Arrange: non-member-only products should be purchasable by anyone
            var rule = new MemberOnlyProductRule(isMemberOnly: false);

            var ctx = new PricingContext
            {
                IsMember = false
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsEligible.Should().BeTrue();
            result.BlockReason.Should().BeNull();
        }
    }
}
