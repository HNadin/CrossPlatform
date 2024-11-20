using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }
        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public string MerchantId { get; set; }
        [ForeignKey("RefTransactionType")]
        public string TransactionTypeCode { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal TransactionAmount { get; set; }
        public string OtherDetails { get; set; }
        public string TransactionTypeDescription { get; set; }
    }
}
