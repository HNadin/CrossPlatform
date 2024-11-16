namespace Lab6.Models
{
    public class Branch
    {
        public Guid BranchId { get; set; }
        public Guid BankId { get; set; }
        public Guid AddressId { get; set; }
        public string BranchTypeCode { get; set; }
        public string BranchDetails { get; set; }
        public Bank Bank { get; set; }
        public Address Address { get; set; }
        public RefBranchType RefBranchType { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
