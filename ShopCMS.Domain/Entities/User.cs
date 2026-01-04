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

        public string PasswordHash { get; set; } = string.Empty; // PBKDF2 hash
        public bool IsActive { get; set; } = true;

        // برای invalidate کردن همه access token ها
        public int TokenVersion { get; set; } = 0;

        // Navigation
        public ICollection<SaleEligibilityRequest> Requests { get; set; } = new List<SaleEligibilityRequest>();
        }
    
}
