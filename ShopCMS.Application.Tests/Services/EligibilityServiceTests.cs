using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using ShopCMS.Application.Services;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Application.Tests.Services
{
    public class EligibilityServiceTests
    {
        [Fact]
        public async Task CheckAsync_ShouldReturnNotEligible_WhenUserIsNotLoggedIn()
        {
            // Arrange: create service instance
            var service = new EligibilityService();

            // Arrange: user is NOT logged in
            var ctx = new EligibilityContext
            {
                IsLoggedIn = false,
                UserRole = "Consumer",
                IsMember = false,
                ProductIsMemberOnly = false
            };

            // Act: check eligibility
            var result = await service.CheckAsync(ctx);

            // Assert: user should NOT be eligible
            result.IsEligible.Should().BeFalse();
            result.Reason.Should().Be("User must be logged in");
        }

        [Fact]
        public async Task CheckAsync_ShouldReturnNotEligible_WhenProductIsMemberOnly_AndUserIsNotMember()
        {
            // Arrange
            var service = new EligibilityService();

            // Arrange: product is members-only but user is not a member
            var ctx = new EligibilityContext
            {
                IsLoggedIn = true,
                UserRole = "Consumer",
                IsMember = false,
                ProductIsMemberOnly = true
            };

            // Act
            var result = await service.CheckAsync(ctx);

            // Assert
            result.IsEligible.Should().BeFalse();
            result.Reason.Should().Be("Product is for members only");
        }

        [Fact]
        public async Task CheckAsync_ShouldReturnEligible_WhenAllConditionsPass()
        {
            // Arrange
            var service = new EligibilityService();

            // Arrange: user satisfies all eligibility conditions
            var ctx = new EligibilityContext
            {
                IsLoggedIn = true,
                UserRole = "Consumer",
                IsMember = true,
                ProductIsMemberOnly = true
            };

            // Act
            var result = await service.CheckAsync(ctx);

            // Assert: eligibility should pass
            result.IsEligible.Should().BeTrue();
            result.Reason.Should().BeNull();
        }
    }
}
