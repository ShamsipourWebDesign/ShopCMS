using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Application.Contracts
{
    public interface ICurrencyRateProvider
    {
       
            Task<decimal> GetRateAsync(
                string baseCurrency,
                string quoteCurrency,
                CancellationToken cancellationToken = default);
        
    }
}
