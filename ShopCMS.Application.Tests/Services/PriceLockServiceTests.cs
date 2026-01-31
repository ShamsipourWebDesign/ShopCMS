using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

using ShopCMS.Application.Services;
using ShopCMS.Domain.Entities.PriceLockApi;
using ShopCMS.Domain.Interfaces;

namespace ShopCMS.Application.Tests.Services
{
    public class PriceLockServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldSaveLock_AndReturnNonEmptyId()
        {
            // Arrange: mock the repository to isolate the service from persistence concerns
            var repo = new Mock<IPriceLockApiRepository>();
            var service = new PriceLockService(repo.Object);

            // Arrange: input data for creating a new price lock
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var finalPrice = 12345m;
            var ttl = TimeSpan.FromMinutes(10);

            // Arrange: capture the entity passed to the repository to validate saved values
            PriceLockApi? savedEntity = null;

            repo.Setup(r => r.SaveAsync(It.IsAny<PriceLockApi>()))
                .Callback<PriceLockApi>(pl => savedEntity = pl)
                .Returns(Task.CompletedTask);

            // Act: create a price lock
            var id = await service.CreateAsync(productId, userId, finalPrice, ttl);

            // Assert: a valid identifier is returned and the entity is persisted once
            id.Should().NotBe(Guid.Empty);

            savedEntity.Should().NotBeNull();
            savedEntity!.ProductId.Should().Be(productId);
            savedEntity.UserId.Should().Be(userId);
            savedEntity.Price.Should().Be(finalPrice);

            repo.Verify(r => r.SaveAsync(It.IsAny<PriceLockApi>()), Times.Once);
        }

        [Fact]
        public async Task GetLockedPriceAsync_ShouldThrow_WhenNoValidLockFound()
        {
            // Arrange: repository returns null when no valid lock exists for the given product/user
            var repo = new Mock<IPriceLockApiRepository>();
            var service = new PriceLockService(repo.Object);

            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            repo.Setup(r => r.GetValidAsync(productId, userId))
                .ReturnsAsync((PriceLockApi?)null);

            // Act: attempt to get a locked price when no lock exists
            Func<Task> act = async () => await service.GetLockedPriceAsync(productId, userId);

            // Assert: service enforces the invariant that a lock must exist
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("No valid price lock found.");
        }

        [Fact]
        public async Task GetLockedPriceAsync_ShouldThrow_WhenLockIsExpired()
        {
            // Arrange: simulate an expired lock (negative TTL means it is already expired)
            var repo = new Mock<IPriceLockApiRepository>();
            var service = new PriceLockService(repo.Object);

            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var expiredLock = new PriceLockApi(productId, userId, 999m, TimeSpan.FromMinutes(-1));

            repo.Setup(r => r.GetValidAsync(productId, userId))
                .ReturnsAsync(expiredLock);

            // Act: attempt to get a locked price from an expired lock
            Func<Task> act = async () => await service.GetLockedPriceAsync(productId, userId);

            // Assert: expired locks must not be accepted
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Price lock has expired.");
        }

        [Fact]
        public async Task GetLockedPriceAsync_ShouldReturnPrice_WhenLockIsValid()
        {
            // Arrange: repository returns a valid (non-expired) lock
            var repo = new Mock<IPriceLockApiRepository>();
            var service = new PriceLockService(repo.Object);

            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var validLock = new PriceLockApi(productId, userId, 777m, TimeSpan.FromMinutes(5));

            repo.Setup(r => r.GetValidAsync(productId, userId))
                .ReturnsAsync(validLock);

            // Act: retrieve locked price
            var price = await service.GetLockedPriceAsync(productId, userId);

            // Assert: service returns the locked price when the lock is valid
            price.Should().Be(777m);
        }
    }
}
