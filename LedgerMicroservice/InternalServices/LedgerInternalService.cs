using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices
{
    public class LedgerInternalService
    {
        public TransactionInm[] GetTransactions()
        {
            return
            [
                new TransactionInm
                {
                    AccountName = "4202", BalanceChange = 813.5f, Id = 859, 
                    Time = new DateTime(2024, 08, 13, 09, 01, 42)
                }
            ];
        }
    }
}
