using LedgerMicroservice.Models;
using Npgsql;

namespace LedgerMicroservice.InternalServices.Db
{
    public class TestPostgresDb(NpgsqlConnection connection) : ILowLevelDb
    {
        private readonly NpgsqlConnection connection = connection;

        public async Task AddAsync<T>(T entity)
        {
            if(entity is null) throw new ArgumentNullException(nameof(entity));

            if (entity is TransactionDbm transaction)
                await AddTransaction(transaction);
            else 
                throw new ArgumentException($"Entity [{entity}] has unexpected type [{entity.GetType()}].");
        }

        public async Task DropEverythingAsync()
        {
            using var command = new NpgsqlCommand("delete from transactions;", connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<TransactionDbm[]> GetTransactionsAsync() => await GetTransactionsInternalAsync();

        public async Task SaveTransactionAsync(TransactionSaveDbm transaction) => await AddAsync(transaction.ToDbm());

        public async Task<T[]> GetAllAsync<T>()
        {
            if (typeof(T) == typeof(TransactionDbm))
                return (await GetTransactionsInternalAsync()).Cast<T>().ToArray();
            else
                throw new InvalidOperationException($"The type [{typeof(T)}] is unexpected.");
        }

        private async Task AddTransaction(TransactionDbm model)
        {
            var sql = "INSERT INTO transactions (time, balance_change, account_name) VALUES (@time, @balance_change, @account_name)";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@time", model.Time);
            command.Parameters.AddWithValue("@balance_change", model.BalanceChange);
            command.Parameters.AddWithValue("@account_name", model.AccountName ?? (object)DBNull.Value);
            await command.ExecuteNonQueryAsync();
        }

        private async Task<TransactionDbm[]> GetTransactionsInternalAsync()
        {
            var transactions = new List<TransactionDbm>();
            using var command = new NpgsqlCommand("SELECT id, time, balance_change, account_name FROM transactions", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                var transaction = new TransactionDbm
                {
                    Id = reader.GetInt32(0),
                    Time = reader.GetDateTime(1),
                    BalanceChange = reader.GetFloat(2),
                    AccountName = reader.IsDBNull(3) ? null : reader.GetString(3)
                };
                transactions.Add(transaction);
            }
            return [.. transactions];
        }
    }
}