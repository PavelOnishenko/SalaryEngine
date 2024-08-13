using LedgerMicroservice.InternalServices;

namespace UnitTests_LedgerMicroservice
{
    public class Tests
    {
        [Test]
        public void LedgerInternalService_MainScenario()
        {
            var service = new LedgerInternalService();

            var result = service.GetTransactions();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(1));
            var element = result.Single();
            Assert.That(element, Is.Not.Null);
            Assert.That(element.Id, Is.EqualTo(859));
            Assert.That(element.BalanceChange, Is.EqualTo(813.5f));
            Assert.That(element.Time.Year, Is.EqualTo(2024));
            Assert.That(element.Time.Month, Is.EqualTo(8));
            Assert.That(element.Time.Day, Is.EqualTo(13));
            Assert.That(element.Time.Hour, Is.EqualTo(9));
            Assert.That(element.Time.Minute, Is.EqualTo(1));
            Assert.That(element.Time.Second, Is.EqualTo(42));
            Assert.That(element.AccountName, Is.EqualTo("4202"));
        }
    }
}