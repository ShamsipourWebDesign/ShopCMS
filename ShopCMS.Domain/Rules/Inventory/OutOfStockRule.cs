using ShopCMS.Domain.Pricing;
using ShopCMS.Domain.Rules;

namespace SalesDecisionEngine.Domain.Rules.Inventory
{
    public class OutOfStockRule : IRule<PricingContext, PricingResult>
    {
        public string Name => "Out Of Stock Rule";
        public string Description => "Blocks purchase when quantity equals zero";

        public PricingResult Evaluate(PricingContext context)
        {
            if (context.Quantity <= 0)
            {
                return new PricingResult
                {
                    IsEligible = false,
                    BlockReason = "Product is out of stock"
                };
            }

            return new PricingResult { IsEligible = true };
        }
    }
}
