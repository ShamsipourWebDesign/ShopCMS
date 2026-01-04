using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
    public class RefreshToken
    {
       
        
            public Guid Id { get; set; }

            public int UserId { get; set; }

            public string TokenHash { get; set; } = string.Empty;

            public DateTime CreatedAtUtc { get; set; }
            public DateTime ExpiresAtUtc { get; set; }

            public DateTime? RevokedAtUtc { get; set; }
            public string? ReplacedByTokenHash { get; set; }

            public bool IsActive =>
                RevokedAtUtc == null && DateTime.UtcNow < ExpiresAtUtc;
        

    }
}
