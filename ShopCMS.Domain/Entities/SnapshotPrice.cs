using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
   
        public class SnapshotPrice
        {
        
        public int SnapshotId { get; set; }

            public int ProductId { get; set; }

            public string CurrencyCode { get; set; }

            public decimal BasePrice { get; set; }

            public decimal FinalPrice { get; set; }

            public decimal ExchangeRate { get; set; }

            public string AppliedRulesJson { get; set; }

            public DateTime CreatedAt { get; set; }

            // Navigation
            public Product Product { get; set; }

            public Currency Currency { get; set; }
        }
    

}
