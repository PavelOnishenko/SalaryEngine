using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices.Db
{
    public class InMemoryDb : ILowLevelDb
    {
        readonly List<TransactionDbm> transactions = [];

        public async Task<TransactionDbm[]> GetTransactionsAsync() => [.. transactions];

        public async Task SaveTransactionAsync(TransactionSaveDbm transaction)
        {
            var dbm = transaction.ToDbm();
            dbm.Id = transactions.Max(x => x.Id) + 1;
            await AddTransactionAsync(dbm);
        }
        public async Task AddTransactionAsync(TransactionDbm transaction) => transactions.Add(transaction);

        public async Task DropEverythingAsync() => transactions.Clear();
    }
}