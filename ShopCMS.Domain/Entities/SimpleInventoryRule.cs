using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
    
        public class SimpleInventoryRule
        {
       
        public int InventoryRuleId { get; set; }

            public int ProductId { get; set; }

            public int AvailableQuantity { get; set; }

            public int SaleThreshold { get; set; }

            public bool OutOfSaleFlag { get; set; }

            // Navigation
            public Product Product { get; set; }
        }
   

}
