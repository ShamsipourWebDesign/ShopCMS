using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
   
        public class PriceLock
        {
        
        public int PriceLockId { get; set; }

            public int ProductId { get; set; }

            public string CurrencyCode { get; set; }

            public decimal FinalPrice { get; set; }

            public DateTime ExpiresAt { get; set; }

            public bool IsUsed { get; set; }

            // Navigation
            public Product Product { get; set; }

            public Currency Currency { get; set; }
        }
    

}
