using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices.Db
{
    public interface IDb
    {
        Task<TransactionDbm[]> GetTransactionsAsync();

        Task SaveTransactionAsync(TransactionSaveDbm transaction);
    }
}