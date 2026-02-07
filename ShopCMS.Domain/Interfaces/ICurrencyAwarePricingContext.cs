using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Domain.Interfaces
{
    public interface ICurrencyAwarePricingContext
    {
        string TargetCurrency { get; }
    }
}
