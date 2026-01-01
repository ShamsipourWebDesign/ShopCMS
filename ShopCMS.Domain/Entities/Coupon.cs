using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
        public class Coupon
        {
        
        public int CouponId { get; set; }

            public string CouponCode { get; set; }

            public int DiscountRuleId { get; set; }

            public DateTime ExpireDate { get; set; }

            public int? UsageLimit { get; set; }

            public bool IsActive { get; set; }

            // Navigation
            public DiscountRule DiscountRule { get; set; }
        }
    

}
