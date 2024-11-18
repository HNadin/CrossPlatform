using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Bank
    {
        [Key]
        public Guid BankId { get; set; }
        public string BankDetails { get; set; }
        public ICollection<Branch> Branches { get; set; }
    }
}
