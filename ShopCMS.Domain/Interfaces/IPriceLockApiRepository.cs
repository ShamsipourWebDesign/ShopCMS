using ShopCMS.Domain.Entities;
using ShopCMS.Domain.Entities.PriceLockApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Interfaces
{
    public interface IPriceLockApiRepository
    {
        Task SaveAsync(PriceLockApi priceLock);
        Task<PriceLockApi?> GetValidAsync(Guid productId, Guid userId);
    }
}
