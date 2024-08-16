using LedgerMicroservice.Models;
using Npgsql;

namespace LedgerMicroservice.InternalServices
{
    public class TestPostgresDb : ILowLevelDb
    {
        private readonly NpgsqlConnection connection;

        public TestPostgresDb(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task AddTransactionAsync(TransactionDbm model)
        {
            var sql = "INSERT INTO transactions (time, balance_change, account_name) VALUES (@time, @balance_change, @account_name)";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@time", model.Time);
                command.Parameters.AddWithValue("@balance_change", model.BalanceChange);
                command.Parameters.AddWithValue("@account_name", model.AccountName ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DropEverythingAsync()
        {
            var sql = "delete from transactions;";
            using var command = new NpgsqlCommand(sql, connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<TransactionDbm[]> GetTransactionsAsync()
        {
            var transactions = new List<TransactionDbm>();
            using (var command = new NpgsqlCommand("SELECT id, time, balance_change, account_name FROM transactions", connection))
            using (var reader = await command.ExecuteReaderAsync())
                while (reader.Read())
                {
                    var transaction = new TransactionDbm
                    {
                        Id = reader.GetInt32(0), Time = reader.GetDateTime(1), BalanceChange = reader.GetFloat(2), 
                        AccountName = reader.IsDBNull(3) ? null : reader.GetString(3)
                    };
                    transactions.Add(transaction);
                }
            return [.. transactions];
        }

        public async Task SaveTransactionAsync(TransactionSaveDbm transaction) => await AddTransactionAsync(transaction.ToDbm());
    }
}