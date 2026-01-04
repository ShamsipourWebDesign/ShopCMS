namespace ShopCMS.Domain.SaleEligibility
{
    public class EligibilityContext
    {
        public bool IsLoggedIn { get; set; }
        public bool IsMember { get; set; }
        public string UserRole { get; set; } = "Consumer";
        public bool IsBanned { get; set; }

        public bool ProductIsMemberOnly { get; set; }
    }
}
