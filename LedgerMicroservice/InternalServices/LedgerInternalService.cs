using LedgerMicroservice.InternalServices.Db;
using LedgerMicroservice.Models;

namespace LedgerMicroservice.InternalServices
{
    public class LedgerInternalService(IDb db)
    {
        private readonly IDb db = db;

        public async Task<TransactionInm[]> GetTransactionsAsync()
        {
            var dbms = await db.GetTransactionsAsync();
            var result = dbms.Select(x => x.ToInm()).ToArray();
            return result;
        }
    }
}
