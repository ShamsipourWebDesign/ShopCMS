using System;
using System.Linq;
using ShopCMS.Domain.Volatility;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Domain.Rules.Volatility
{
    public class CryptoAndFiatVolatilityRule : IRule<VolatilityContext, VolatilityResult>
    {
        public string Name => "Crypto & Fiat Volatility Guard";
        public string Description => "Blocks purchase when USD/USDT/ETH has high volatility";

        private static readonly string[] Symbols = { "USD", "USDT", "ETH" };

        public VolatilityResult Evaluate(VolatilityContext context)
        {
            foreach (var symbol in Symbols)
            {
                var history = context.History
                    .Where(h => h.Symbol == symbol)
                    .OrderBy(h => h.Timestamp)
                    .ToList();

                if (!history.Any())
                    continue;

                var latestTime = history.Last().Timestamp;
                var windowStart = latestTime - context.TimeWindow;

                var windowData = history
                    .Where(h => h.Timestamp >= windowStart)
                    .ToList();

                if (windowData.Count < 2)
                    continue;

                var first = windowData.First().Price;
                var last = windowData.Last().Price;

                if (first == 0)
                    continue;

                var changePercent = ((last - first) / first) * 100m;

                if (Math.Abs(changePercent) >= context.ThresholdPercentage)
                {
                    return new VolatilityResult
                    {
                        IsBlocked = true,
                        Symbol = symbol,
                        ChangePercent = Math.Round(changePercent, 2),
                        Message = $"High volatility detected in {symbol}: {Math.Round(changePercent, 2)}%"
                    };
                }
            }

            return new VolatilityResult
            {
                IsBlocked = false,
                Message = "No critical volatility detected"
            };
        }
    }
}
