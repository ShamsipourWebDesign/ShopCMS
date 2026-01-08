using ShopCMS.Domain.Entities;
using ShopCMS.Domain.Entities.PriceLockApi;
using ShopCMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Application.Services
{


    public class PriceLockService
    {
        private readonly IPriceLockApiRepository _repository;

        public PriceLockService(IPriceLockApiRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateAsync(
            Guid productId,
            Guid userId,
            decimal finalPrice,
            TimeSpan ttl)
        {
            var lockApi = new PriceLockApi(
                productId,
                userId,
                finalPrice,
                ttl);

            await _repository.SaveAsync(lockApi);
            return lockApi.Id;
        }

        public async Task<decimal> GetLockedPriceAsync(
           Guid productId,
           Guid userId)
        {
            var lockApi = await _repository.GetValidAsync(productId, userId);

            if (lockApi == null)
                throw new InvalidOperationException("No valid price lock found.");

            lockApi.EnsureNotExpired();
            return lockApi.Price;




        }

    }
}
