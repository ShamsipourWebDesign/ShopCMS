namespace ShopCMS.Domain.Volatility
{
    public class VolatilityResult
    {
        public bool IsBlocked { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal ChangePercent { get; set; }
    }
}
