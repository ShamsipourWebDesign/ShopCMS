using System;
using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Volatility;
using ShopCMS.Domain.Rules.Volatility;

namespace ShopCMS.Domain.Tests.Volatility
{
    public class PriceLockExpirationRuleTests
    {
        [Fact]
        public void Evaluate_ShouldBeInvalid_WhenPriceLockIsAlreadyUsed()
        {
            // Arrange: used price lock is always invalid (even if not expired)
            var rule = new PriceLockExpirationRule();

            var ctx = new PriceLockContext
            {
                IsUsed = true,
                Now = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Reason.Should().Be("Price lock already used");
        }

        [Fact]
        public void Evaluate_ShouldBeInvalid_WhenNowIsAfterExpiresAt()
        {
            // Arrange: expired price lock is invalid
            var rule = new PriceLockExpirationRule();

            var ctx = new PriceLockContext
            {
                IsUsed = false,
                Now = new DateTime(2026, 1, 1, 10, 0, 1, DateTimeKind.Utc),
                ExpiresAt = new DateTime(2026, 1, 1, 10, 0, 0, DateTimeKind.Utc)
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Reason.Should().Be("Price lock has expired");
        }

        [Fact]
        public void Evaluate_ShouldBeValid_WhenNowIsEqualToExpiresAt()
        {
            // Arrange: rule checks "Now > ExpiresAt", so equality is still valid
            var rule = new PriceLockExpirationRule();

            var t = new DateTime(2026, 1, 1, 10, 0, 0, DateTimeKind.Utc);

            var ctx = new PriceLockContext
            {
                IsUsed = false,
                Now = t,
                ExpiresAt = t
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Reason.Should().Be("Price lock is valid");
        }
    }
}
