using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices.Db
{
    public class InMemoryDb : ILowLevelDb
    {
        private readonly List<TransactionDbm> transactions = [];

        public Task<TransactionDbm[]> GetTransactionsAsync() => Task.FromResult<TransactionDbm[]>([.. transactions]);

        public async Task SaveTransactionAsync(TransactionSaveDbm transaction) => await AddAsync((TransactionDbm?)transaction.ToDbm());

        public Task AddAsync<T>(T entity)
        {
            if(entity is null) 
                throw new ArgumentNullException(nameof(entity));

            if (entity is TransactionDbm entityDbm)
            {
                entityDbm.Id = transactions.Count == 0 ? 1 : transactions.Max(x => x.Id) + 1;
                transactions.Add(entityDbm);
            }
            else
            {
                throw new ArgumentException($"Entity [{entity}] has unexpected type [{entity.GetType()}].");
            }
            return Task.CompletedTask;
        }

        public Task DropEverythingAsync()
        {
            transactions.Clear();
            return Task.CompletedTask;
        }

        public Task<T[]> GetAllAsync<T>()
        {
            if(typeof(T) == typeof(TransactionDbm)) 
                return Task.FromResult(transactions.ToArray().Cast<T>().ToArray());
            else
                throw new InvalidOperationException($"The type [{typeof(T)}] is unexpected.");
        }
    }
}