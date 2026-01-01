using System;
using System.Collections.Generic;

namespace ShopCMS.Domain.Volatility
{
    public class VolatilityContext
    {
        // History of rates over a period of time
        public List<CurrencyRatePoint> History { get; set; } = new();

        // Threshold percentage (e.g. 5 means 5%)
        public decimal ThresholdPercentage { get; set; }

        // Review time frame (e.g. 10 minutes)
        public TimeSpan TimeWindow { get; set; }
    }
}
