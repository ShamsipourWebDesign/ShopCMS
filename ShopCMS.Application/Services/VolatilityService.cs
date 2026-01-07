using System;
using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Application.Contracts;
using ShopCMS.Domain.Volatility;
using ShopCMS.Application.Services.Providers;

namespace ShopCMS.Application.Services
{
    /// <summary>
    /// Service that checks volatility using a fake price provider.
    /// Simple & stable for university projects.
    /// </summary>
    public class VolatilityService : IVolatilityService
    {
        private readonly FakeVolatilityProvider _provider;

        public VolatilityService(FakeVolatilityProvider provider)
        {
            _provider = provider;
        }

        public async Task<VolatilityResult> CheckAsync(
            VolatilityContext context,
            CancellationToken cancellationToken = default)
        {
            // Get two simulated price points
            var price1 = await _provider.GetPriceAsync(context.Symbol);
            await Task.Delay(200, cancellationToken); // small gap
            var price2 = await _provider.GetPriceAsync(context.Symbol);

            if (price1 == 0)
            {
                return new VolatilityResult
                {
                    IsBlocked = true,
                    Symbol = context.Symbol,
                    Message = "Invalid price data received"
                };
            }

            // percentage change
            var changePercent = ((price2 - price1) / price1) * 100m;

            return new VolatilityResult
            {
                Symbol = context.Symbol,
                ChangePercent = Math.Round(changePercent, 2),
                IsBlocked = Math.Abs(changePercent) >= context.ThresholdPercentage,
                Message = "Simulated volatility evaluation completed"
            };
        }
    }
}
