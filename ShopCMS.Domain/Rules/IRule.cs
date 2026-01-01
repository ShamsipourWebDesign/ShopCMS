namespace ShopCMS.Domain.Rules
{
    public interface IRule<TContext, TResult>
    {
        string Name { get; }
        string Description { get; }
        TResult Evaluate(TContext context);
    }
}
