using LedgerMicroservice.Models;

namespace LedgerMicroservice
{
    public static class Extensions
    {
        public static TransactionDbm ToDbm(this TransactionSaveDbm source) => 
            new() { AccountName = source.AccountName, BalanceChange = source.BalanceChange, Time = source.Time };

        public static TransactionInm ToInm(this TransactionDbm source) => 
            new()
                { AccountName = source.AccountName, BalanceChange = source.BalanceChange, Time = source.Time, Id = source.Id };
    }
}