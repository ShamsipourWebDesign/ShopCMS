using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{
    
        public class User
        {
        
        public int UserId { get; set; }

            public string Username { get; set; }

            public string Role { get; set; } // Admin / Consumer

            // Navigation
            public ICollection<SaleEligibilityRequest> Requests { get; set; } = new List<SaleEligibilityRequest>();
        }
    
}
