using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
   
        public class ExchangeRate
        {
        
        public int ExchangeRateId { get; set; }

            public string CurrencyCode { get; set; }

            public decimal Rate { get; set; }

            public DateTime RetrievedAt { get; set; }

            public string ProviderName { get; set; }

            // Navigation
            public Currency Currency { get; set; }
        }
    

}
