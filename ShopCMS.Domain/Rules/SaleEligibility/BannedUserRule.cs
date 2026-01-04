using ShopCMS.Domain.Rules;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Domain.SaleEligibility.Rules
{
    public class BannedUserRule : IRule<EligibilityContext, EligibilityResult>
    {
        public string Name => "Banned user";
        public string Description => "If account is banned, block the purchase.";

        public EligibilityResult Evaluate(EligibilityContext context)
        {
            if (context.IsBanned)
            {
                return new EligibilityResult
                {
                    IsEligible = false,
                    Reason = "User account is banned"
                };
            }

            return new EligibilityResult { IsEligible = true };
        }
    }
}
