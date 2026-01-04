namespace ShopCMS.Domain.SaleEligibility
{
    public class EligibilityResult
    {
        public bool IsEligible { get; set; } = true;
        public string? Reason { get; set; }
    }
}
