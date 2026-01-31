using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

using ShopCMS.Application.Services;
using ShopCMS.Domain.Pricing;

namespace ShopCMS.Application.Tests.Services
{
    public class PricingServiceTests
    {
        [Fact]
        public async Task CalculatePriceAsync_ShouldReturnBasePrice_WhenTargetCurrencyIsIRR()
        {
            // Arrange: PricingService builds the default pricing rule pipeline internally
            var service = new PricingService();

            // Arrange: Provide a fully populated context to ensure all rules can run safely
            var context = new PricingContext
            {
                BasePrice = 1_000_000m,

                // Discount-related fields
                PercentageDiscount = 10m,
                FixedDiscount = 50_000m,
                IsMember = true,
                MemberDiscount = 20_000m,
                HasCoupon = true,
                CouponAmount = 30_000m,

                // IRR => no currency conversion should happen (conversion rule returns base price)
                TargetCurrency = "IRR",
                TargetCurrencyRate = 0m
            };

            // Act: calculate final price
            var result = await service.CalculatePriceAsync(context);

            // Assert: With IRR, conversion rule returns base price
            result.FinalPrice.Should().Be(1_000_000m);

            // Assert: Explainability - applied rule names should exist in output
            result.AppliedRules.Should().Contain("Base Price Rule");
            result.AppliedRules.Should().Contain("Percentage Discount Rule");
            result.AppliedRules.Should().Contain("Fixed Discount Rule");
            result.AppliedRules.Should().Contain("Member Discount Rule");
            result.AppliedRules.Should().Contain("Coupon Discount Rule");
            result.AppliedRules.Should().Contain("Max Discount Cap Rule");
            result.AppliedRules.Should().Contain("Time Promotion Rule");
            result.AppliedRules.Should().Contain("Currency Conversion Rule");
        }

        [Fact]
        public async Task CalculatePriceAsync_ShouldConvertPrice_WhenTargetCurrencyIsNotIRR()
        {
            // Arrange
            var service = new PricingService();

            // Arrange: Set non-IRR target currency and provide a conversion rate
            var context = new PricingContext
            {
                BasePrice = 1_000_000m,
                TargetCurrency = "USD",
                TargetCurrencyRate = 50_000m // 1,000,000 / 50,000 = 20.00
            };

            // Act
            var result = await service.CalculatePriceAsync(context);

            // Assert: Conversion rule divides by rate and rounds to 2 decimals
            result.FinalPrice.Should().Be(20.00m);

            // Assert: Conversion rule adds a descriptive message for traceability
            result.AppliedRules.Should().Contain(x => x.Contains("Converted to USD"));

            // Assert: Rule names should appear in applied rules (appended by rule engine)
            result.AppliedRules.Should().Contain("Currency Conversion Rule");
            result.AppliedRules.Should().Contain("Base Price Rule");
        }

        [Fact]
        public async Task CalculatePriceAsync_ShouldRoundToTwoDecimals_AfterConversion()
        {
            // Arrange
            var service = new PricingService();

            // Arrange: Use a rate that produces a repeating decimal
            var context = new PricingContext
            {
                BasePrice = 100m,
                TargetCurrency = "USD",
                TargetCurrencyRate = 3m // 100 / 3 = 33.3333 -> 33.33 (rounded)
            };

            // Act
            var result = await service.CalculatePriceAsync(context);

            // Assert: rounding is applied to 2 decimals
            result.FinalPrice.Should().Be(33.33m);
        }

        [Fact]
        public async Task CalculatePriceAsync_ShouldAppendAllPricingRuleNames_InFactoryOrder()
        {
            // Arrange: PricingService uses PricingRulesFactory to build the pipeline
            var service = new PricingService();

            // Arrange: Use a non-IRR currency to ensure conversion rule runs at the end
            var context = new PricingContext
            {
                BasePrice = 1_000_000m,
                TargetCurrency = "USD",
                TargetCurrencyRate = 50_000m
            };

            // Act
            var result = await service.CalculatePriceAsync(context);

            // Assert: The rule engine appends rule names in the exact execution order
            result.AppliedRules.Should().ContainInOrder(new[]
            {
                "Base Price Rule",
                "Percentage Discount Rule",
                "Fixed Discount Rule",
                "Member Discount Rule",
                "Coupon Discount Rule",
                "Max Discount Cap Rule",
                "Time Promotion Rule",
                "Currency Conversion Rule"
            });
        }

        [Fact]
        public async Task CalculatePriceAsync_ShouldReturnEligibleResult_ByDefault()
        {
            // Arrange: Pricing rules do not block pricing by default
            var service = new PricingService();

            var context = new PricingContext
            {
                BasePrice = 250_000m,
                TargetCurrency = "IRR"
            };

            // Act
            var result = await service.CalculatePriceAsync(context);

            // Assert: pricing should remain eligible
            result.IsEligible.Should().BeTrue();
            result.BlockReason.Should().BeNull();
        }
    }
}
