using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Domain.Rules.Eligibility
{
    public class RoleBasedAccessRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Role Based Access Rule";
        public string Description => "Allows only specific user roles to buy this product";

        private readonly List<string> _allowedRoles;

        public RoleBasedAccessRule(params string[] roles)
        {
            _allowedRoles = roles.ToList();
        }

        public PricingResult Evaluate(PricingContext context)
        {
            if (!_allowedRoles.Contains(context.UserRole))
            {
                return new PricingResult
                {
                    IsEligible = false,
                    BlockReason = "User role is not allowed to buy this product"
                };
            }

            return new PricingResult { IsEligible = true };
        }
    }
}
