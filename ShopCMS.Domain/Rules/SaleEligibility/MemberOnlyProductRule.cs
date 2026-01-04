using ShopCMS.Domain.Rules;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Domain.SaleEligibility.Rules
{
    public class MemberOnlyProductRule : IRule<EligibilityContext, EligibilityResult>
    {
        public string Name => "Member-only product";
        public string Description => "If product is member-only and user is not member, block.";

        public EligibilityResult Evaluate(EligibilityContext context)
        {
            if (context.ProductIsMemberOnly && !context.IsMember)
            {
                return new EligibilityResult
                {
                    IsEligible = false,
                    Reason = "Product is for members only"
                };
            }

            return new EligibilityResult { IsEligible = true };
        }
    }
}
