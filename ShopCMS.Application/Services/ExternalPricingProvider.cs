using ShopCMS.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Application.Services
{
    public class ExternalPricingProvider
    {
        private readonly ICurrencyRateProvider _currencyProvider;

        public ExternalPricingProvider(ICurrencyRateProvider currencyProvider)
        {
            _currencyProvider = currencyProvider;
        }

        public async Task<decimal> CalculateAsync(decimal basePrice)
        {
            var rate = await _currencyProvider.GetRateAsync("USD", "IRR");
            return basePrice * rate;
        }
    }
}
