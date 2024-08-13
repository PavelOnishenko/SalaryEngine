namespace LedgerMicroservice.Models
{
    public class TransactionSaveDbm
    {
        public DateTime Time { get; set; }

        public float BalanceChange { get; set; }

        public string? AccountName { get; set; }
    }
}