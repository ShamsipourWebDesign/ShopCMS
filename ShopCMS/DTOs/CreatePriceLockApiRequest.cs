namespace ShopCMS.DTOs
{
    public class CreatePriceLockApiRequest
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
