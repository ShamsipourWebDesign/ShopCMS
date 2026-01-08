using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Infrastructure.PriceLock
{
    public class PriceLockApiEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

}
