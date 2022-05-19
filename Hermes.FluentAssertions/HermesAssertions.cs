using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Hermes.Core;

namespace Hermes.FluentAssertions
{
    public class HermesAssertions : ReferenceTypeAssertions<HermesServer, HermesAssertions>
    {
        public HermesAssertions(HermesServer instance)
            : base(instance)
        {
        }

        protected override string Identifier => "hermes";

        public AndConstraint<HermesAssertions> MessageReceived(string from, string to, string subject, string body, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(HermesContainsMessage(from, to, subject, body))
                .FailWith("Your email does not exist");

            return new AndConstraint<HermesAssertions>(this);
        }

        private bool HermesContainsMessage(string from, string to, string subject, string body) => Subject.Contains(from, to, subject, body);
    }

}