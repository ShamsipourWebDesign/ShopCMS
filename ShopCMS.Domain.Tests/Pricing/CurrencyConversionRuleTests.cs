using FluentAssertions;
using Xunit;

using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules.Pricing;

namespace ShopCMS.Domain.Tests.Rules.Pricing
{
    public class CurrencyConversionRuleTests
    {
        [Fact]
        public void Evaluate_ShouldReturnBasePrice_WhenTargetCurrencyIsIRR()
        {
            // Arrange: IRR means "no conversion"
            var rule = new CurrencyConversionRule();

            var ctx = new PricingContext
            {
                BasePrice = 1_000_000m,
                TargetCurrency = "IRR",
                TargetCurrencyRate = 0m
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert: final price must remain base price
            result.FinalPrice.Should().Be(1_000_000m);
        }

        [Fact]
        public void Evaluate_ShouldConvertAndRoundToTwoDecimals_WhenTargetCurrencyIsNotIRR()
        {
            // Arrange: conversion should divide by rate and round to 2 decimals
            var rule = new CurrencyConversionRule();

            var ctx = new PricingContext
            {
                BasePrice = 100m,
                TargetCurrency = "USD",
                TargetCurrencyRate = 3m // 100 / 3 = 33.3333 -> 33.33
            };

            // Act
            var result = rule.Evaluate(ctx);

            // Assert
            result.FinalPrice.Should().Be(33.33m);
        }
    }
}
