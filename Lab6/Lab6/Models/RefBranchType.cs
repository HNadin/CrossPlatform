using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class RefBranchType
    {
        [Key]
        public string BranchTypeCode { get; set; }
        public string BranchTypeDescription { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }
}
