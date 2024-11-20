using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class TimeController : Controller
    {
        private readonly Lab6APIService _lab6APIService;

        public TimeController(Lab6APIService lab6APIService)
        {
            _lab6APIService = lab6APIService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string inputDate)
        {
            try
            {
                if (DateTime.TryParse(inputDate, out DateTime parsedDateTime))
                {
                    string ukrainianTime = await _lab6APIService.ConvertTimeAsync(parsedDateTime);
                    ViewBag.UkrainianTime = ukrainianTime;
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid date format. Please enter a valid date.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
