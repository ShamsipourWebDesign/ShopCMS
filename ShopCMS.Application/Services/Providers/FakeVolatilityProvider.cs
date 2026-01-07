using System.Threading.Tasks;

namespace ShopCMS.Application.Services.Providers
{
    /// <summary>
    /// Very simple fake price provider.
    /// No Random, no external API – just fixed values per symbol.
    /// </summary>
    public class FakeVolatilityProvider
    {
        public Task<decimal> GetPriceAsync(string symbol)
        {
            decimal price;

            // fixed demo prices per symbol
            if (symbol == "BTCUSDT")
                price = 50000m;
            else if (symbol == "ETHUSDT")
                price = 3000m;
            else if (symbol == "USDTIRR")
                price = 60000m;
            else
                price = 100m; // default for anything else

            return Task.FromResult(price);
        }
    }
}
