using System.Collections.Generic;
using ShopCMS.Domain.Rules;
using ShopCMS.Domain.Rules.Volatility;
using ShopCMS.Domain.Volatility;

namespace ShopCMS.Application.Services.Composition
{
    // Builds default rule-set for volatility engine
    public static class VolatilityRulesFactory
    {
        public static List<IRule<VolatilityContext, VolatilityResult>> CreateDefaultRules()
        {
            return new List<IRule<VolatilityContext, VolatilityResult>>
            {
                new CryptoAndFiatVolatilityRule()
            };
        }
    }
}
