using System;

namespace ShopCMS.Domain.Volatility
{
    public class CurrencyRatePoint
    {
        // "USD", "USDT", "ETH"
        public string Symbol { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
