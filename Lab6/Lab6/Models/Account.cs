using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models
{
    public class Account
    {
        [Key]
        public int AccountNumber { get; set; }
        public string AccountStatusCode { get; set; }
        [ForeignKey("RefAccountType")]
        public string AccountTypeCode { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public decimal CurrentBalance { get; set; }
        public string OtherDetails { get; set; }
        public Customer Customer { get; set; }
        public RefAccountType RefAccountType { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
