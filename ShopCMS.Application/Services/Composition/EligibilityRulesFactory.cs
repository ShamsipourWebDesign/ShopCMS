using System.Collections.Generic;
using ShopCMS.Domain.Rules;
using ShopCMS.Domain.SaleEligibility;
using ShopCMS.Domain.SaleEligibility.Rules;




namespace ShopCMS.Application.Services.Composition
{
    public static class EligibilityRulesFactory
    {
        public static List<IRule<EligibilityContext, EligibilityResult>> CreateDefaultRules()
        {
            return new List<IRule<EligibilityContext, EligibilityResult>>
            {
                new UserMustBeLoggedInRule(),
                new RoleBasedAccessRule(),
                new MemberOnlyProductRule(),
            };
        }
    }
}
