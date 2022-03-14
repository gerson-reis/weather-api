using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace weather.Integrations.Tests
{
    internal class Configurations
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            // TODO: add your test dependencies here

            return services;
        }
    }
}
