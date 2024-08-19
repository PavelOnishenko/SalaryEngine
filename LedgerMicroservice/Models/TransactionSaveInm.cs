namespace LedgerMicroservice.Models
{
    public class TransactionSaveInm
    {
        public DateTime Time { get; set; }

        public float BalanceChange { get; set; }

        public string? AccountName { get; set; }
    }
}