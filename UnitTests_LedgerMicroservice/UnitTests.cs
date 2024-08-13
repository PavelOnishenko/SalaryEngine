using LedgerMicroservice.InternalServices;
using LedgerMicroservice.Models;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests_LedgerMicroservice
{
    public class Tests
    {
        [Test]
        public void LedgerInternalService_MainScenario()
        {
            var serviceProvider = GetServiceProvider();
            var db = serviceProvider.GetRequiredService<IDb>() as InMemoryDb;
            var entity = new TransactionDbm
                { AccountName = "4202", BalanceChange = 813.5f, Id = 859, Time = new DateTime(2024, 08, 13, 09, 01, 42) };
            db!.AddTransaction(entity);
            var service = serviceProvider.GetRequiredService<LedgerInternalService>();

            var result = service.GetTransactions();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(1));
            var element = result.Single();
            Assert.That(element, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(element.Id, Is.EqualTo(859));
                Assert.That(element.BalanceChange, Is.EqualTo(813.5f));
                Assert.That(element.Time.Year, Is.EqualTo(2024));
                Assert.That(element.Time.Month, Is.EqualTo(8));
                Assert.That(element.Time.Day, Is.EqualTo(13));
                Assert.That(element.Time.Hour, Is.EqualTo(9));
                Assert.That(element.Time.Minute, Is.EqualTo(1));
                Assert.That(element.Time.Second, Is.EqualTo(42));
                Assert.That(element.AccountName, Is.EqualTo("4202"));
            });
        }

        private static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IDb, InMemoryDb>();
            serviceCollection.AddTransient<LedgerInternalService>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}