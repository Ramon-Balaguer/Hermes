using Hermes.Core;

namespace Hermes.FluentAssertions
{

    public static class HermesExtensions
    {
        public static HermesAssertions Should(this HermesServer instance)
        {
            return new HermesAssertions(instance);
        }
    }
}