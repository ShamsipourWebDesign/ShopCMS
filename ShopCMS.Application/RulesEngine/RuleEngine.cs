using System.Collections.Generic;
using ShopCMS.Application.Contracts;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Application.RulesEngine
{
    public class RuleEngine<TContext, TResult> : IRuleEngine<TContext, TResult>
        where TResult : new()
    {
        private readonly IEnumerable<IRule<TContext, TResult>> _rules;

        public RuleEngine(IEnumerable<IRule<TContext, TResult>> rules)
        {
            _rules = rules;
        }

        public TResult Execute(TContext context, TResult result)
        {
            foreach (var rule in _rules)
            {
                var ruleResult = rule.Evaluate(context);

                result = ruleResult;

                var blockProperty = typeof(TResult).GetProperty("IsEligible");

                if (blockProperty != null)
                {
                    var value = blockProperty.GetValue(result);

                    if (value is bool eligible && !eligible)
                        break;
                }
            }

            return result;
        }
    }
}
