using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models
{
    public class Branch
    {
        [Key]
        public Guid BranchId { get; set; }
        [ForeignKey("Bank")]
        public Guid BankId { get; set; }
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }
        [ForeignKey("RefBranchType")]
        public string BranchTypeCode { get; set; }
        public string BranchDetails { get; set; }
        public Bank Bank { get; set; }
        public Address Address { get; set; }
        public RefBranchType RefBranchType { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
