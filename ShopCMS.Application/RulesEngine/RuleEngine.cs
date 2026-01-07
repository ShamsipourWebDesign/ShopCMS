using System.Collections.Generic;
using ShopCMS.Application.Contracts;
using ShopCMS.Domain.Rules;

namespace ShopCMS.Application.RulesEngine
{
    public class RuleEngine<TContext, TResult>
        where TResult : new()
    {
        private readonly IEnumerable<IRule<TContext, TResult>> _rules;

        public RuleEngine(IEnumerable<IRule<TContext, TResult>> rules)
        {
            _rules = rules;
        }

        public TResult Execute(TContext context, TResult result)
        {
            // temporary list to store applied rule names
            var appliedRuleNames = new List<string>();

            foreach (var rule in _rules)
            {
                // run rule
                var ruleResult = rule.Evaluate(context);

                // capture applied rule name (if any)
                if (!string.IsNullOrWhiteSpace(rule.Name))
                    appliedRuleNames.Add(rule.Name);

                // assign latest result
                result = ruleResult;

                // check IsEligible stop condition (optional)
                var blockProperty = typeof(TResult).GetProperty("IsEligible");

                if (blockProperty != null)
                {
                    var value = blockProperty.GetValue(result);

                    if (value is bool eligible && !eligible)
                        break;
                }
            }

            // attach applied rules list if TResult contains AppliedRules property
            var appliedRulesProperty = typeof(TResult).GetProperty("AppliedRules");

            if (appliedRulesProperty != null)
            {
                var existingList =
                    appliedRulesProperty.GetValue(result) as IList<string>;

                if (existingList != null)
                {
                    foreach (var name in appliedRuleNames)
                        existingList.Add(name);
                }
            }

            return result;
        }
    }
}
