using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

using ShopCMS.Application.Services;
using ShopCMS.Domain.Pricing;
using ShopCMS.Application.Contracts;

namespace ShopCMS.Application.Tests.Services
{
    public class PricingServiceTests
    {
        private static PricingService CreateService()
        {
            var currencyRateProvider = new Mock<ICurrencyRateProvider>();
            return new PricingService(currencyRateProvider.Object);
        }

        [Fact]
        public async Task CalculatePriceAsync_ShouldReturnBasePrice_WhenTargetCurrencyIsIRR()
        {
            var service = CreateService();

            var context = new PricingContext
            {
                BasePrice = 1_000_000m,
                PercentageDiscount = 10m,
                FixedDiscount = 50_000m,
                IsMember = true,
                MemberDiscount = 20_000m,
                HasCoupon = true,
                CouponAmount = 30_000m,
                TargetCurrency = "IRR",
                TargetCurrencyRate = 0m
            };

            var result = await service.CalculatePriceAsync(context);

            result.FinalPrice.Should().Be(1_000_000m);
        }

        [Fact]
        public async Task CalculatePriceAsync_ShouldConvertPrice_WhenTargetCurrencyIsNotIRR()
        {
            var service = CreateService();

            var context = new PricingContext
            {
                BasePrice = 1_000_000m,
                TargetCurrency = "USD",
                TargetCurrencyRate = 50_000m
            };

            var result = await service.CalculatePriceAsync(context);

            result.FinalPrice.Should().Be(20.00m);
        }

        [Fact]
        public async Task CalculatePriceAsync_ShouldRoundToTwoDecimals_AfterConversion()
        {
            var service = CreateService();

            var context = new PricingContext
            {
                BasePrice = 100m,
                TargetCurrency = "USD",
                TargetCurrencyRate = 3m
            };

            var result = await service.CalculatePriceAsync(context);

            result.FinalPrice.Should().Be(33.33m);
        }

        [Fact]
        public async Task CalculatePriceAsync_ShouldAppendAllPricingRuleNames_InFactoryOrder()
        {
            var service = CreateService();

            var context = new PricingContext
            {
                BasePrice = 1_000_000m,
                TargetCurrency = "USD",
                TargetCurrencyRate = 50_000m
            };

            var result = await service.CalculatePriceAsync(context);

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
            var service = CreateService();

            var context = new PricingContext
            {
                BasePrice = 250_000m,
                TargetCurrency = "IRR"
            };

            var result = await service.CalculatePriceAsync(context);

            result.IsEligible.Should().BeTrue();
            result.BlockReason.Should().BeNull();
        }
    }
}
