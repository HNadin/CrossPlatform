using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class SearchController : Controller
    {
        private readonly Lab6APIService _lab6APIService;

        public SearchController(Lab6APIService lab6APIService)
        {
            _lab6APIService = lab6APIService;
        }

        // GET: /search
        [Route("/search")]
        public async Task<IActionResult> Index(DateTime? date, string? transactionTypes, string? valueStart, string? valueEnd)
        {

            // Конвертуємо список типів транзакцій в List<string>
            List<string>? transactionTypeList = null;
            if (!string.IsNullOrEmpty(transactionTypes))
            {
                transactionTypeList = transactionTypes.Split(',').ToList();
            }

            // Викликаємо сервіс для пошуку
            var results = await _lab6APIService.SearchTransactionsAsync(date, transactionTypeList, valueStart, valueEnd);
            return View(results);
        }
    }
}