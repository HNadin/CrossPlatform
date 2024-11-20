using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class RefAccountType
    {
        [Key]
        public string AccountTypeCode { get; set; }
        public string AccountTypeDescription { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
