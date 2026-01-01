using System;

namespace ShopCMS.Domain.Pricing
{
    public class PricingContext
    {
        // Base product price in IRR
        public decimal BasePrice { get; set; }

        // percentage discount (like 10 => means 10%)
        public decimal PercentageDiscount { get; set; }

        // fixed discount (e.g., 50000 toman)
        public decimal FixedDiscount { get; set; }

        // coupon
        public bool HasCoupon { get; set; }
        public decimal CouponAmount { get; set; }

        // member status
        public bool IsMember { get; set; }
        public decimal MemberDiscount { get; set; }

        // promotion
        public DateTime? PromoStart { get; set; }
        public DateTime? PromoEnd { get; set; }
        public decimal PromoDiscount { get; set; }

        // discount cap
        public decimal TotalDiscount { get; set; }
        public decimal MaxDiscountCap { get; set; }

        // multi currency
        public string TargetCurrency { get; set; } = "IRR";
        public decimal TargetCurrencyRate { get; set; }

        // crypto & forex volatility support
        public decimal UsdRate { get; set; }
        public decimal UsdtRate { get; set; }
        public decimal EthRate { get; set; }

        // quantity (for inventory / per order rules)
        public int Quantity { get; set; }

        // user info (for eligibility rules)
        public bool IsLoggedIn { get; set; }
        public string UserRole { get; set; } = "Consumer";
        public bool IsAccountBlocked { get; set; }
    }
}
