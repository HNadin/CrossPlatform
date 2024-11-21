using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class Lab6AccountController : Controller
    {
        private readonly Lab6APIService _lab6APIService;

        public Lab6AccountController(Lab6APIService lab6APIService)
        {
            _lab6APIService = lab6APIService;
        }

        // GET: /account
        [Route("/account")]
        public async Task<IActionResult> Index(string apiVersion = "1.0")
        {
            var accounts = apiVersion == "2.0"
                ? await _lab6APIService.GetAccountsV2Async()
                : await _lab6APIService.GetAccountsV1Async();

            return View(accounts);
        }

        // GET: /account/details/{id}
        [Route("/account/details/{id}")]
        public async Task<IActionResult> Details(int id, string apiVersion = "1.0")
        {
            var account = apiVersion == "2.0"
                ? await _lab6APIService.GetAccountDetailsV2Async(id)
                : await _lab6APIService.GetAccountDetailsV1Async(id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
    }
}