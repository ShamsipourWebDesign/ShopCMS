using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class CurrencyConversionRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Currency Conversion Rule";
        public string Description => "Converts price into USD / USDT / ETH";

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.TargetCurrency == "IRR")
                return new PricingResult { FinalPrice = context.BasePrice };

            var converted = context.BasePrice / context.TargetCurrencyRate;

            return new PricingResult
            {
                FinalPrice = Math.Round(converted, 2),
                AppliedRules = { $"Converted to {context.TargetCurrency}" }
            };
        }
    }
}
