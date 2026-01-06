using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Domain.Rules.Eligibility
{
    public class UserMustBeLoggedInRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "User Login Required Rule";
        public string Description => "Blocks purchase if user is not logged in";

        public PricingResult Evaluate(PricingContext context)
        {
            if (!context.IsLoggedIn)
            {
                return new PricingResult
                {
                    IsEligible = false,
                    BlockReason = "User must be logged in to purchase"
                };
            }

            return new PricingResult { IsEligible = true };
        }
    }
}
