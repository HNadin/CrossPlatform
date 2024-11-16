namespace Lab6.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public int AccountNumber { get; set; }
        public string MerchantId { get; set; }
        public string TransactionTypeCode { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal TransactionAmount { get; set; }
        public string OtherDetails { get; set; }
        public Account Account { get; set; }
        public RefTransactionType RefTransactionType { get; set; }
    }
}
