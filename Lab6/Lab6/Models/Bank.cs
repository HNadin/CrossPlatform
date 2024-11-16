namespace Lab6.Models
{
    public class Bank
    {
        public Guid BankId { get; set; }
        public string BankDetails { get; set; }
        public ICollection<Branch> Branches { get; set; }
    }
}
