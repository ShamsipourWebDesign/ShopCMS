namespace ShopCMS.Domain.Volatility
{
    public class PriceLockResult
    {
        public bool IsValid { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
