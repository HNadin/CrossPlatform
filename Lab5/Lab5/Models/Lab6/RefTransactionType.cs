using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class RefTransactionType
    {
        [Key]
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeDescription { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
