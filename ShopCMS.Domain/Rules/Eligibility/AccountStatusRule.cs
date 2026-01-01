using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace SalesDecisionEngine.Domain.Rules.Eligibility
{
    public class AccountStatusRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Account Status Rule";
        public string Description => "Blocks purchase if user account is blocked";

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.IsAccountBlocked)
            {
                return new PricingResult
                {
                    IsEligible = false,
                    BlockReason = "User account is blocked"
                };
            }

            return new PricingResult { IsEligible = true };
        }
    }
}
