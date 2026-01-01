using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
   
        public class Currency
        {
        
        public string CurrencyCode { get; set; }

            public string Name { get; set; }

            public bool IsEnabled { get; set; }

        }
    

}
