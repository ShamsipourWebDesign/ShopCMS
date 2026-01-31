using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

using ShopCMS.Application.Services;
using ShopCMS.Application.Services.Providers;
using ShopCMS.Domain.Volatility;

namespace ShopCMS.Application.Tests.Services
{
    public class VolatilityServiceTests
    {
        [Fact]
        public async Task CheckAsync_ShouldNotBlock_WhenChangeIsBelowThreshold()
        {
            // Arrange: use a fake volatility provider to avoid external dependencies
            var provider = new FakeVolatilityProvider();
            var service = new VolatilityService(provider);

            // Arrange: define a context with a positive threshold
            var context = new VolatilityContext
            {
                Symbol = "BTCUSDT",
                ThresholdPercentage = 1m
            };

            // Act: evaluate volatility
            var result = await service.CheckAsync(context);

            // Assert: no blocking should occur when change is below threshold
            result.IsBlocked.Should().BeFalse();
            result.ChangePercent.Should().Be(0m);
        }

        [Fact]
        public async Task CheckAsync_ShouldBlock_WhenChangeMeetsOrExceedsThreshold()
        {
            // Arrange: fake provider always returns zero change
            var provider = new FakeVolatilityProvider();
            var service = new VolatilityService(provider);

            // Arrange: zero threshold means any change triggers blocking
            var context = new VolatilityContext
            {
                Symbol = "ETHUSDT",
                ThresholdPercentage = 0m
            };

            // Act
            var result = await service.CheckAsync(context);

            // Assert: zero change >= zero threshold => blocked
            result.IsBlocked.Should().BeTrue();
            result.ChangePercent.Should().Be(0m);
        }
    }
}
