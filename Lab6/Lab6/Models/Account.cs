namespace Lab6.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string AccountStatusCode { get; set; }
        public string AccountTypeCode { get; set; }
        public Guid CustomerId { get; set; }
        public decimal CurrentBalance { get; set; }
        public string OtherDetails { get; set; }
        public Customer Customer { get; set; }
        public RefAccountType RefAccountType { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
