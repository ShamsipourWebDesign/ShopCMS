using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Domain.Rules.Eligibility
{
    public class MemberOnlyProductRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Member Only Product Rule";
        public string Description => "Blocks purchase if product is member-only and user is not a member";

        private readonly bool _isMemberOnlyProduct;

        public MemberOnlyProductRule(bool isMemberOnly)
        {
            _isMemberOnlyProduct = isMemberOnly;
        }

        public PricingResult Evaluate(PricingContext context)
        {
            if (_isMemberOnlyProduct && !context.IsMember)
            {
                return new PricingResult
                {
                    IsEligible = false,
                    BlockReason = "This product is only available for members"
                };
            }

            return new PricingResult { IsEligible = true };
        }
    }
}
