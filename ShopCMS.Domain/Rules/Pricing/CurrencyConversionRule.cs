using ShopCMS.Domain.Pricing;

namespace ShopCMS.Domain.Rules.Pricing
{
    public class CurrencyConversionRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Currency Conversion Rule";
        public string Description => "Converts the product price into the target currency (USD, USDT, ETH, etc.) based on the given exchange rate.";

        public PricingResult Evaluate(PricingContext context)
        {
            // If the target currency is 'IRR' (Iranian Rial), no conversion is applied
            if (context.TargetCurrency == "IRR")
                return new PricingResult 
                { 
                    FinalPrice = context.BasePrice,
                    AppliedRules = { "No conversion applied (Currency is IRR)" }
                };

            // Perform conversion using the exchange rate provided
            var converted = context.BasePrice / context.TargetCurrencyRate;

            return new PricingResult
            {
                FinalPrice = Math.Round(converted, 2),
                AppliedRules = { $"Converted to {context.TargetCurrency} at rate {context.TargetCurrencyRate}" }
            };
        }
    }
}
