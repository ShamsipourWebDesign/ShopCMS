using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Entities
{

    public class SaleEligibilityRequest
    {

        public int RequestId { get; set; }

        public int ProductId { get; set; }

        public int? UserId { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime RequestedAt { get; set; }

        // Navigation
        public Product Product { get; set; }

        public User User { get; set; }

        public Currency Currency { get; set; }

        public SaleDecisionResult DecisionResult { get; set; }
    }


}
