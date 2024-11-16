using System.Net;

namespace Lab6.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public Guid BranchId { get; set; }
        public string PersonalDetails { get; set; }
        public string ContactDetails { get; set; }
        public Branch Branch { get; set; }
        public Address Address { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
