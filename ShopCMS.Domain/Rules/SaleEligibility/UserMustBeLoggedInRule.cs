using ShopCMS.Domain.Rules;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Domain.SaleEligibility.Rules
{
    public class UserMustBeLoggedInRule : IRule<EligibilityContext, EligibilityResult>
    {
        public string Name => "User must be logged in";
        public string Description => "If user is not authenticated, block the purchase.";

        public EligibilityResult Evaluate(EligibilityContext context)
        {
            if (!context.IsLoggedIn)
            {
                return new EligibilityResult
                {
                    IsEligible = false,
                    Reason = "User must be logged in"
                };
            }

            return new EligibilityResult { IsEligible = true };
        }
    }
}
