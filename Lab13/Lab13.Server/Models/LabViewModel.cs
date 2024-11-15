using System.ComponentModel.DataAnnotations;

namespace Lab13.Server.Models
{
    public class TestCase
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }

    public class LabViewModel
    {
        [Required]
        public IFormFile InputFile { get; set; }
        public string InputContent { get; set; }
        public string OutputContent { get; set; }

        public string LabNumber { get; set; }
        public string Description { get; set; }
        public string Variant { get; set; }
        public string InputDescription { get; set; }
        public string OutputDescription { get; set; }
        public List<TestCase> TestCases { get; set; }
    }
}