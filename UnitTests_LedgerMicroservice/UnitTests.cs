using LedgerMicroservice.InternalServices;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests_LedgerMicroservice
{
    public class UnitTests : TestBase
    {
        protected override ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<InMemoryDb>();
            serviceCollection.AddSingleton<ILowLevelDb>(sp => sp.GetRequiredService<InMemoryDb>());
            serviceCollection.AddSingleton<IDb>(sp => sp.GetRequiredService<InMemoryDb>());
            serviceCollection.AddTransient<LedgerInternalService>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}