using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Application.Contracts
{
    public interface IRuleEvaluator
    {
        Task<TResult> EvaluateAsync<TContext, TResult>(
            TContext context,
            IEnumerable<IRule<TContext, TResult>> rules,
            CancellationToken cancellationToken = default);
    }
}
