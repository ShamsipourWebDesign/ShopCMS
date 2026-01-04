using Microsoft.EntityFrameworkCore;
using ShopCMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace ShopCMS.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<SimpleInventoryRule> SimpleInventoryRules { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<DiscountRule> DiscountRules { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<SaleEligibilityRequest> SaleEligibilityRequests { get; set; }
        public DbSet<SaleDecisionResult> SaleDecisionResults { get; set; }
        public DbSet<AppliedRule> AppliedRules { get; set; }
        public DbSet<SnapshotPrice> SnapshotPrices { get; set; }
        public DbSet<PriceLock> PriceLocks { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
