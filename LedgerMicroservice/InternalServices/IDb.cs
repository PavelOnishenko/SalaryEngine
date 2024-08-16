using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices
{
    public interface IDb
    {
        Task<TransactionDbm[]> GetTransactionsAsync();

        Task SaveTransactionAsync(TransactionSaveDbm transaction);
    }
}