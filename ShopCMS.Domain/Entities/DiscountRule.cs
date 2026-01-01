using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
   
        public class DiscountRule
        {
        
        public int DiscountRuleId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string DiscountType { get; set; } // Percent / Fixed

            public decimal Value { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? EndDate { get; set; }

            public int? MaxUsagePerUser { get; set; }

            public decimal? MinPurchaseAmount { get; set; }

            public bool IsActive { get; set; }

            public int Version { get; set; }

            // Navigation

            // n-n products via join
            public ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();

            // 1-n coupons
            public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
        }
    

}
