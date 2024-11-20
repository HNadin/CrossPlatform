using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class CentralTableController : Controller
    {
        private readonly Lab6APIService _lab6APIService;

        public CentralTableController(Lab6APIService lab6APIService)
        {
            _lab6APIService = lab6APIService;
        }

        // GET: Transactions
        [Route("/transactions")]
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var transactions = await _lab6APIService.GetTransactionsAsync();
            return View(transactions);
        }

        // GET: Transactions/Details/{id}
        [Route("/transactions/details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var transaction = await _lab6APIService.GetTransactionAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }
    }
}