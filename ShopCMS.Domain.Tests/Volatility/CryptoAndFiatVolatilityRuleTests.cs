using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Volatility;
using ShopCMS.Domain.Rules.Volatility;

namespace ShopCMS.Domain.Tests.Volatility
{
    public class CryptoAndFiatVolatilityRuleTests
    {
        [Fact]
        public void Evaluate_ShouldNotBlock_WhenNoHistoryExists()
        {
            // Arrange: when there is no history, the rule should not block
            var rule = new CryptoAndFiatVolatilityRule();

            var ctx = new VolatilityContext
            {
                ThresholdPercentage = 5m,
                TimeWindow = TimeSpan.FromMinutes(10),
                History = new List<CurrencyRatePoint>() // empty history
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsBlocked.Should().BeFalse();
            result.Message.Should().Be("No critical volatility detected");
        }

        [Fact]
        public void Evaluate_ShouldBlock_WhenVolatilityExceedsThreshold_ForUSD()
        {
            // Arrange: USD changes by 10% within the window => must be blocked (threshold 5%)
            var rule = new CryptoAndFiatVolatilityRule();

            var now = DateTime.UtcNow;

            var ctx = new VolatilityContext
            {
                ThresholdPercentage = 5m,
                TimeWindow = TimeSpan.FromMinutes(10),
                History = new List<CurrencyRatePoint>
                {
                    new CurrencyRatePoint { Symbol = "USD", Price = 100m, Timestamp = now.AddMinutes(-9) },
                    new CurrencyRatePoint { Symbol = "USD", Price = 110m, Timestamp = now.AddMinutes(-1) } // +10%
                }
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: core behavior
            result.IsBlocked.Should().BeTrue();
            result.Symbol.Should().Be("USD");
            result.ChangePercent.Should().Be(10.00m);

            // Assert: message formatting may vary (10% vs 10.00%), so validate key content
            result.Message.Should().Contain("High volatility detected in USD:");
            result.Message.Should().Contain("10");
        }

        [Fact]
        public void Evaluate_ShouldNotBlock_WhenWindowHasLessThanTwoPoints()
        {
            // Arrange: if the window has < 2 points for a symbol, the rule cannot compute change
            var rule = new CryptoAndFiatVolatilityRule();

            var now = DateTime.UtcNow;

            var ctx = new VolatilityContext
            {
                ThresholdPercentage = 5m,
                TimeWindow = TimeSpan.FromMinutes(10),
                History = new List<CurrencyRatePoint>
                {
                    new CurrencyRatePoint { Symbol = "USDT", Price = 100m, Timestamp = now.AddMinutes(-5) }
                    // only one point => should not block
                }
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsBlocked.Should().BeFalse();
            result.Message.Should().Be("No critical volatility detected");
        }

        [Fact]
        public void Evaluate_ShouldIgnoreSymbol_WhenFirstPriceIsZero()
        {
            // Arrange: if first price is 0, the rule must avoid division by zero and skip blocking
            var rule = new CryptoAndFiatVolatilityRule();

            var now = DateTime.UtcNow;

            var ctx = new VolatilityContext
            {
                ThresholdPercentage = 1m,
                TimeWindow = TimeSpan.FromMinutes(10),
                History = new List<CurrencyRatePoint>
                {
                    new CurrencyRatePoint { Symbol = "ETH", Price = 0m,   Timestamp = now.AddMinutes(-9) },
                    new CurrencyRatePoint { Symbol = "ETH", Price = 200m, Timestamp = now.AddMinutes(-1) }
                }
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsBlocked.Should().BeFalse();
            result.Message.Should().Be("No critical volatility detected");
        }

        [Fact]
        public void Evaluate_ShouldRoundChangePercent_ToTwoDecimals()
        {
            // Arrange: verify rounding behavior (Math.Round(changePercent, 2))
            var rule = new CryptoAndFiatVolatilityRule();

            var now = DateTime.UtcNow;

            // changePercent = ((103.333 - 100) / 100) * 100 = 3.333 -> 3.33
            var ctx = new VolatilityContext
            {
                ThresholdPercentage = 3m, // ensure it blocks
                TimeWindow = TimeSpan.FromMinutes(10),
                History = new List<CurrencyRatePoint>
                {
                    new CurrencyRatePoint { Symbol = "USD", Price = 100m,     Timestamp = now.AddMinutes(-9) },
                    new CurrencyRatePoint { Symbol = "USD", Price = 103.333m, Timestamp = now.AddMinutes(-1) }
                }
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.IsBlocked.Should().BeTrue();
            result.Symbol.Should().Be("USD");
            result.ChangePercent.Should().Be(3.33m);

            // The message should include the percentage text (format may vary), so check key content
            result.Message.Should().Contain("High volatility detected in USD:");
            result.Message.Should().Contain("3.33");
        }
    }
}
