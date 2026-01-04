using ShopCMS.Domain.Rules;
using ShopCMS.Domain.SaleEligibility;

namespace ShopCMS.Domain.SaleEligibility.Rules
{
    public class RoleBasedAccessRule : IRule<EligibilityContext, EligibilityResult>
    {
        public string Name => "Role-based access";
        public string Description => "Restrict purchase based on user role.";

        public EligibilityResult Evaluate(EligibilityContext context)
        {
            // اینجا هر منطقی که دوست داری می‌تونی بذاری
            // فعلاً یک مثال ساده:

            if (context.UserRole == "Banned")
            {
                return new EligibilityResult
                {
                    IsEligible = false,
                    Reason = "User role is not allowed to purchase this product"
                };
            }

            return new EligibilityResult { IsEligible = true };
        }
    }
}
