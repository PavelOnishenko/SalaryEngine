using LedgerMicroservice.InternalServices;
using LedgerMicroservice.InternalServices.Db;
using LedgerMicroservice.Models;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests_LedgerMicroservice
{
    public abstract class TestBase
    {
        protected abstract ServiceProvider GetServiceProvider();

        [Test]
        public async Task LedgerInternalService_GetTransactions_MainScenario()
        {
            var serviceProvider = GetServiceProvider();
            var db = serviceProvider.GetRequiredService<ILowLevelDb>();
            await db.DropEverythingAsync();
            var entity = new TransactionDbm
            { AccountName = "4202", BalanceChange = 813.5f, Id = 859, Time = new DateTime(2024, 08, 13, 09, 01, 42) };
            await db!.AddAsync(entity);
            var service = serviceProvider.GetRequiredService<LedgerInternalService>();

            var result = await service.GetTransactionsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(1));
            var element = result.Single();
            Assert.That(element, Is.Not.Null);
            Assert.Multiple(() =>
            {
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

        [Test]
        public async Task LedgerInternalService_SaveTransaction_MainScenario()
        {
            var serviceProvider = GetServiceProvider();
            var service = serviceProvider.GetRequiredService<LedgerInternalService>();
            var now = new DateTime(2024, 8, 19, 9, 17, 0);
            var model = new TransactionSaveInm
            {
                AccountName = "916",
                BalanceChange = 8f,
                Time = now
            };
            var db = serviceProvider.GetRequiredService<ILowLevelDb>();
            await db.DropEverythingAsync();

            await service.SaveTransactionAsync(model);

            var dbModels = await db.GetAllAsync<TransactionDbm>();
            Assert.That(dbModels, Has.Length.EqualTo(1));
            var dbModel = dbModels.Single();
            Assert.Multiple(() =>
            {
                Assert.That(dbModel.BalanceChange, Is.EqualTo(8f));
                Assert.That(dbModel.Id, Is.Not.Zero);
                Assert.That(dbModel.AccountName, Is.EqualTo("916"));
                Assert.That(dbModel.Time, Is.EqualTo(now));
            });
        }
    }
}