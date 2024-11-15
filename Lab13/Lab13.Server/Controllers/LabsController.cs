using Lab13.Server.Models;
using LabsLibrary;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabsController : ControllerBase
    {
        [HttpGet("lab1")]
        public IActionResult GetLab1()
        {
            var model = new LabViewModel
            {
                LabNumber = "1",
                Variant = "2",
                Description = "Якось, прийшовши додому зі школи, Світлана виявила записку від мами, в якій вона просила зробити салат. " +
                              "Світлана знала, що салат - це суміш двох або більше інгредієнтів, тому їй не склало труднощів виконати мамине прохання. " +
                              "Але Світлана хоче стати математиком, тому, для тренування, вирішила порахувати, скільки різних салатів вона зможе зробити з наявних продуктів (майонез, огірки, помідори). " +
                              "Після невеликих розрахунків отримала відповідь: 4.\r\nЗнаючи, що ви любите цікаві завдання, і хочете стати програмістами, Світлана попросила вас написати програму, яка визначає кількість різних салатів для довільної кількості інгредієнтів.",
                InputDescription = "Вхідний файл INPUT.TXT містить натуральне число N – кількість наявних інгредієнтів (N < 32).",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть кількість різних салатів.",
                TestCases = new List<TestCase>
                {
                    new TestCase { Input = "3", Output = "4" },
                    new TestCase { Input = "4", Output = "11" }
                }
            };
            return Ok(model);
        }

        [HttpGet("lab2")]
        public IActionResult GetLab2()
        {
            var model = new LabViewModel
            {
                LabNumber = "2",
                Variant = "2",
                Description = "Для проведення експерименту треба вибрати N наявних приладів тільки три. " +
                              "Для цього виконують наступну операцію - якщо в групі приладів більше трьох, то їх нумерують і вибирають одну з груп: з парними чи непарними номерами. " +
                              "Операцію повторюють до тих пір, поки в групі не залишиться три або менше приладів. Якщо їх залишається рівно три, то вони беруться для експерименту.",
                InputDescription = "У єдиному рядку вхідного файлу INPUT.TXT записано число N (1 ≤ N ≤ 2147483647).",
                OutputDescription = "У єдиний рядок вихідного файлу OUTPUT.TXT потрібно вивести одне число - знайдену кількість способів вибору приладів.",
                TestCases = new List<TestCase>
                {
                    new TestCase { Input = "3", Output = "1" },
                    new TestCase { Input = "6", Output = "2" }
                }
            };
            return Ok(model);
        }

        [HttpGet("lab3")]
        public IActionResult GetLab3()
        {
            var model = new LabViewModel
            {
                LabNumber = "3",
                Variant = "2",
                Description = "У неорієнтованому графі потрібно знайти довжину найкоротшого шляху між двома вершинами",
                InputDescription = "У вхідному файлі INPUT.TXT записано спочатку число N - кількість вершин у графі (1 ≤ N ≤ 100)",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть довжину найкоротшого шляху. Якщо шляху немає, виведіть -1.",
                TestCases = new List<TestCase>
                {
                    new TestCase
                    {
                        Input = "5\n0 1 0 0 1\n1 0 1 0 0\n0 1 0 0 0\n0 0 0 0 0\n1 0 0 0 0\n3 5",
                        Output = "3"
                    }
                }
            };
            return Ok(model);
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessLab([FromForm] int labNumber, [FromForm] IFormFile inputFile)
        {
            Console.WriteLine($"Received labNumber: {labNumber}");
            if (inputFile == null || inputFile.Length == 0)
                return BadRequest(new { Error = "Please upload a valid file." });

            if (labNumber < 1 || labNumber > 3)
                return BadRequest(new { Error = "Invalid lab number. Please specify 1, 2, or 3." });

            string[] lines;
            using (var reader = new StreamReader(inputFile.OpenReadStream()))
            {
                var fileContent = await reader.ReadToEndAsync();
                lines = fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            }

            string output;
            try
            {
                switch (labNumber)
                {
                    case 1:
                        output = string.Join(Environment.NewLine, Lab1Helper.ProcessLab1Inputs(lines));
                        break;

                    case 2:
                        var lab2Inputs = lines.Select(line => int.TryParse(line, out var value) ? value : (int?)null)
                            .Where(value => value.HasValue)
                            .Select(value => value.Value)
                            .ToList();

                        output = string.Join(Environment.NewLine, lab2Inputs.Select(input => Lab2Helper.CountLab2Ways(input)));
                        break;

                    case 3:
                        output = Lab3Helper.FindLab3ShortestPath(lines);
                        break;

                    default:
                        return BadRequest(new { Error = "Invalid lab number. Please specify 1, 2, or 3." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error processing lab: {ex.Message}" });
            }

            return Ok(new { Output = output });
        }

    }
}
