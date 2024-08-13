namespace LedgerMicroservice.Models
{
    public class TransactionDbm
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public float BalanceChange { get; set; }

        public string? AccountName { get; set; }
    }
}