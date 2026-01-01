using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
   
        public class ProductPrice
        {
        
        public int ProductPriceId { get; set; }

            public int ProductId { get; set; }

            public string CurrencyCode { get; set; }

            public decimal BasePrice { get; set; }

            public DateTime? ValidFrom { get; set; }

            public DateTime? ValidTo { get; set; }

            public bool IsActive { get; set; }

            // Navigation
            public Product Product { get; set; }
            public Currency Currency { get; set; }
        }
    

}
