using Lab5.Models;
using LabsLibrary;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        public IActionResult Lab1()
        {
            var model = new LabViewModel
            {
                LabNumber = "1",
                Variant = "2",
                Description = "Якось, прийшовши додому зі школи, Світлана виявила записку від мами, в якій вона просила зробити салат. " +
                "Світлана знала, що салат - це суміш двох або більше інгредієнтів, тому їй не склало труднощів виконати мамине прохання.\r\nАле Світлана хоче стати математиком, тому, для тренування, вирішила порахувати, скільки різних салатів вона зможе зробити з наявних продуктів (майонез, огірки, помідори). " +
                "Після невеликих розрахунків отримала відповідь: 4.\r\nЗнаючи, що ви любите цікаві завдання, і хочете стати програмістами, Світлана попросила вас написати програму, яка визначає кількість різних салатів для довільної кількості інгредієнтів.\r\n",
                InputDescription = "Вхідний файл INPUT.TXT містить натуральне число N – кількість наявних інгредієнтів (N < 32).",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть кількість різних салатів.",
                TestCases = new List<TestCase>
            {
                new TestCase { Input = "3", Output = "4" },
                new TestCase { Input = "4", Output = "11" }
            }
            };
            return View("Lab", model);
        }

        public IActionResult Lab2()
        {
            var model = new LabViewModel
            {
                LabNumber = "2",
                Variant = "2",
                Description = "Для проведення експерименту треба вибрати N наявних приладів тільки три. " +
                "Для цього виконують наступну операцію - якщо в групі приладів більше трьох, то їх нумерують і вибирають одну з груп: з парними чи непарними номерами. " +
                "Операцію повторюють до тих пір, поки в групі не залишиться три або менше приладів. Якщо їх залишається рівно три, то вони беруться для експерименту.\r\nПотрібно написати програму, яка підрахує кількість способів вибору приладів.\r\n",
                InputDescription = "У єдиному рядку вхідного файлу INPUT.TXT записано число N (1 ≤ N ≤ 2147483647).",
                OutputDescription = "У єдиний рядок вихідного файлу OUTPUT.TXT потрібно вивести одне число - знайдену кількість способів вибору приладів.",
                TestCases = new List<TestCase>
            {
                new TestCase { Input = "3", Output = "1" },
                new TestCase { Input = "6", Output = "2" }
            }
            };
            return View("Lab", model);
        }

        public IActionResult Lab3()
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
            return View("Lab", model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLab(int labNumber, IFormFile inputFile)
        {
            if (inputFile == null || inputFile.Length == 0)
                return BadRequest("Please upload a valid file.");

            // Read file contents into a string array
            string[] lines;
            using (var reader = new StreamReader(inputFile.OpenReadStream()))
            {
                var fileContent = await reader.ReadToEndAsync();
                lines = fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries); // Split into non-empty lines
            }

            string output = string.Empty;

            // Execute the lab processing method based on lab number
            try
            {
                switch (labNumber)
                {
                    case 1:
                        output = string.Join(Environment.NewLine, Lab1Helper.ProcessLab1Inputs(lines));
                        break;

                    case 2:
                        // Обробляємо кожен елемент масиву окремо
                        var lab2Inputs = lines.Select(line => int.TryParse(line, out var value) ? value : (int?)null)
                            .Where(value => value.HasValue)
                            .Select(value => value.Value)
                            .ToList();

                        foreach (var input in lab2Inputs)
                        {
                            // Для кожного елементу викликаємо метод
                            output += Lab2Helper.CountLab2Ways(input) + Environment.NewLine;
                        }
                        break;

                    case 3:
                        output = Lab3Helper.FindLab3ShortestPath(lines);
                        break;

                    default:
                        return BadRequest("Invalid lab number. Please specify 1, 2, or 3.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing lab: {ex.Message}");
            }

            return Json(new { output });
        }

    }
}
