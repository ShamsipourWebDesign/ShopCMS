using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
   
        public class ProductDiscount
        {
        
            public int ProductId { get; set; }

            public int DiscountRuleId { get; set; }

            // Navigation
            public Product Product { get; set; }

            public DiscountRule DiscountRule { get; set; }
        }
        

}
