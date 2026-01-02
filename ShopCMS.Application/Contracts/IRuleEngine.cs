using System.Threading.Tasks;

namespace ShopCMS.Application.Contracts
{
    public interface IRuleEngine<TContext, TResult>
        where TResult : new()
    {
        TResult Execute(TContext context, TResult initialResult);
    }
}
