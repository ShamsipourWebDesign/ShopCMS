using ShopCMS.Domain.PricingAudit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.PricingAudit
{
    public interface IPricingAuditRepository
    {
        Task SaveAsync(PricingAudit audit, CancellationToken cancellationToken);
    }

}
