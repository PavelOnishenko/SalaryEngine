using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices
{
    public class LedgerInternalService(IDb db)
    {
        private readonly IDb db = db;

        public TransactionInm[] GetTransactions()
        {
            var dbms = db.GetTransactions();
            var result = dbms.Select(x => x.ToInm()).ToArray();
            return result;
        }
    }
}
