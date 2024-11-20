using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Lab6.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }
        [ForeignKey("Branch")]
        public Guid BranchId { get; set; }
        public string PersonalDetails { get; set; }
        public string ContactDetails { get; set; }
        public Branch Branch { get; set; }
        public Address Address { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
