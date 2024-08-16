using LedgerMicroservice.InternalServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace UnitTests_LedgerMicroservice
{
    public class IntegrationTests : TestBase
    {

        protected override ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build().GetConnectionString("DefaultConnection");
            serviceCollection.AddTransient<ILowLevelDb, TestPostgresDb>();
            serviceCollection.AddTransient<IDb, TestPostgresDb>();
            serviceCollection.AddTransient<LedgerInternalService>();
            AddNpgSqlConnection(serviceCollection, connectionString);
            return serviceCollection.BuildServiceProvider();
        }

        private static void AddNpgSqlConnection(ServiceCollection serviceCollection, string? connectionString)
        {
            serviceCollection.AddSingleton(sp =>
            {
                var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                return conn;
            });
        }
    }
}