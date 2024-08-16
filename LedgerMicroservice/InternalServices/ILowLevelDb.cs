using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices
{
    public interface ILowLevelDb : IDb
    {
        Task AddTransactionAsync(TransactionDbm model);

        Task DropEverythingAsync();
    }
}