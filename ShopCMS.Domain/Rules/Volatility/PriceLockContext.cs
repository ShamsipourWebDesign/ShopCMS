using System;

namespace ShopCMS.Domain.Volatility
{
    public class PriceLockContext
    {
        public DateTime Now { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
    }
}
