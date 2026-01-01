using System;
using ShopCMS.Domain.Volatility;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Domain.Rules.Volatility
{
    public class PriceLockExpirationRule : IRule<PriceLockContext, PriceLockResult>
    {
        public string Name => "Price Lock Expiration Rule";
        public string Description => "Checks whether price lock is still valid";

        public PriceLockResult Evaluate(PriceLockContext context)
        {
            if (context.IsUsed)
            {
                return new PriceLockResult
                {
                    IsValid = false,
                    Reason = "Price lock already used"
                };
            }

            if (context.Now > context.ExpiresAt)
            {
                return new PriceLockResult
                {
                    IsValid = false,
                    Reason = "Price lock has expired"
                };
            }

            return new PriceLockResult
            {
                IsValid = true,
                Reason = "Price lock is valid"
            };
        }
    }
}
