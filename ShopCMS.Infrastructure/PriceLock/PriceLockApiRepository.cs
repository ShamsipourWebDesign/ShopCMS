using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopCMS.Domain.Entities.PriceLockApi;
using ShopCMS.Domain.Interfaces;
using ShopCMS.Infrastructure.Persistence.Context;


namespace ShopCMS.Infrastructure.PriceLock
{
    
    

    public class PriceLockApiRepository : IPriceLockApiRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceLockApiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(PriceLockApi priceLock)
        {
            var entity = new PriceLockApiEntity
            {
                Id = priceLock.Id,
                ProductId = priceLock.ProductId,
                UserId = priceLock.UserId,
                Price = priceLock.Price,
                ExpiresAt = priceLock.ExpiresAt
            };

            _context.PriceLockApis.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PriceLockApi?> GetValidAsync(Guid productId, Guid userId)
        {
            var entity = await _context.PriceLockApis.FirstOrDefaultAsync(x =>
                x.ProductId == productId &&
                x.UserId == userId &&
                x.ExpiresAt > DateTime.UtcNow);

            if (entity == null)
                return null;

            return new PriceLockApi(
                entity.ProductId,
                entity.UserId,
                entity.Price,
                entity.ExpiresAt - DateTime.UtcNow);
        }
    }

}
