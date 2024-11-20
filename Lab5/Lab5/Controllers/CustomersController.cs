using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    [Controller]
    public class CustomersController : Controller
    {
        private readonly Lab6APIService _lab6APIService;

        public CustomersController(Lab6APIService lab6APIService)
        {
            _lab6APIService = lab6APIService;
        }

        // GET: Customers
        [Route("/customers")]
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["AccessToken"] ?? "";

            var customers = await _lab6APIService.GetCustomersAsync(token);
            return View(customers);
        }

        // GET: Customers/Details/{id}
        [Route("/customers/details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var customer = await _lab6APIService.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
    }
}