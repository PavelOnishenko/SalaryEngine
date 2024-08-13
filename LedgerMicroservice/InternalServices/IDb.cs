using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices
{
    public interface IDb
    {
        TransactionDbm[] GetTransactions();

        void SaveTransaction(TransactionSaveDbm transaction);
    }
}