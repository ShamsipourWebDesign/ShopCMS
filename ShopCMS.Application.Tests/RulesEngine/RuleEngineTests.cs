using System.Collections.Generic;
using FluentAssertions;
using ShopCMS.Application.RulesEngine;
using ShopCMS.Domain.Rules;
using Xunit;

namespace ShopCMS.Application.Tests.RulesEngine
{
    public class RuleEngineTests
    {
        [Fact]
        public void Execute_ShouldRunRulesInOrder_AndReturnLastRuleResult()
        {
            // Arrange: define a list of rules executed sequentially
            var rules = new List<IRule<string, TestResult>>
            {
                new RuleA(),
                new RuleB(),
                new RuleC()
            };

            // Arrange: create rule engine
            var engine = new RuleEngine<string, TestResult>(rules);

            // Act: execute rules
            var result = engine.Execute("ctx", new TestResult());

            // Assert: last rule result should be returned
            result.Value.Should().Be("C");

            // Assert: rules should be applied in correct order
            result.AppliedRules.Should().ContainInOrder(new[]
            {
                "Rule A",
                "Rule B",
                "Rule C"
            });
        }

        [Fact]
        public void Execute_ShouldStopEarly_WhenIsEligibleBecomesFalse()
        {
            // Arrange: BlockingRule makes IsEligible = false
            var rules = new List<IRule<string, TestResult>>
            {
                new RuleA(),
                new BlockingRule(),
                new RuleC() // This rule must NOT be executed
            };

            var engine = new RuleEngine<string, TestResult>(rules);

            // Act
            var result = engine.Execute("ctx", new TestResult());

            // Assert: execution must stop after BlockingRule
            result.IsEligible.Should().BeFalse();
            result.Value.Should().Be("BLOCKED");

            // Assert: BlockingRule applied, RuleC skipped
            result.AppliedRules.Should().Contain("Blocking Rule");
            result.AppliedRules.Should().NotContain("Rule C");
        }

        // ----------------------------
        // Test helper classes
        // ----------------------------

        private class TestResult
        {
            public string? Value { get; set; }
            public bool IsEligible { get; set; } = true;
            public List<string> AppliedRules { get; set; } = new();
        }

        private class RuleA : IRule<string, TestResult>
        {
            public string Name => "Rule A";
            public string Description => "Test rule A";

            public TestResult Evaluate(string context)
                => new TestResult { Value = "A" };
        }

        private class RuleB : IRule<string, TestResult>
        {
            public string Name => "Rule B";
            public string Description => "Test rule B";

            public TestResult Evaluate(string context)
                => new TestResult { Value = "B" };
        }

        private class RuleC : IRule<string, TestResult>
        {
            public string Name => "Rule C";
            public string Description => "Test rule C";

            public TestResult Evaluate(string context)
                => new TestResult { Value = "C" };
        }

        private class BlockingRule : IRule<string, TestResult>
        {
            public string Name => "Blocking Rule";
            public string Description => "Stops rule pipeline execution";

            public TestResult Evaluate(string context)
                => new TestResult
                {
                    Value = "BLOCKED",
                    IsEligible = false
                };
        }
    }
}
