using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices
{
    public class InMemoryDb : IDb
    {
        readonly List<TransactionDbm> transactions = [];

        public TransactionDbm[] GetTransactions() => [.. transactions];

        public void SaveTransaction(TransactionSaveDbm transaction)
        {
            var dbm = transaction.ToDbm();
            dbm.Id = transactions.Max(x => x.Id) + 1;
            AddTransaction(dbm);
        }

        public void AddTransaction(TransactionDbm transaction) => transactions.Add(transaction);
    }
}