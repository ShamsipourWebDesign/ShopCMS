using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
    
        public class Product
        {
        
        public int ProductId { get; set; }

            public string Name { get; set; }

            public string Sku { get; set; }

            public bool IsActive { get; set; }

            public DateTime? SaleStartDate { get; set; }

            public DateTime? SaleEndDate { get; set; }

            public bool IsMemberOnly { get; set; }

            public DateTime CreatedAt { get; set; }

            // Navigation

            // 1-1 Inventory Rule
            public SimpleInventoryRule InventoryRule { get; set; }

            // 1-n Prices
            public ICollection<ProductPrice> Prices { get; set; } = new List<ProductPrice>();

            // n-n DiscountRule via ProductDiscount
            public ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();

            // 1-n Snapshots
            public ICollection<SnapshotPrice> Snapshots { get; set; } = new List<SnapshotPrice>();
        }
    

}
