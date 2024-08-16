using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices.Db
{
    public interface ILowLevelDb : IDb
    {
        Task AddTransactionAsync(TransactionDbm model);

        Task DropEverythingAsync();
    }
}