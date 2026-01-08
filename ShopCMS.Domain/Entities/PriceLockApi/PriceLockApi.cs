using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities.PriceLockApi
{
    public class PriceLockApi
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Price { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public bool IsExpired => DateTime.UtcNow > ExpiresAt;

        private PriceLockApi() { }

        public PriceLockApi(Guid productId, Guid userId, decimal price, TimeSpan ttl)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            UserId = userId;
            Price = price;
            ExpiresAt = DateTime.UtcNow.Add(ttl);
        }

        public void EnsureNotExpired()
        {
            if (IsExpired)
                throw new InvalidOperationException("Price lock has expired.");
        }
    }
}

