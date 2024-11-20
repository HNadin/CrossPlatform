using Lab5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    [Controller]
    public class AddressesController : Controller
    {
        private readonly Lab6APIService _lab6APIService;

        public AddressesController(Lab6APIService lab6APIService)
        {
            _lab6APIService = lab6APIService;
        }

        // GET: Addresses
        [Route("/adresses")]
        public async Task<IActionResult> Index()
        {

            var addresses = await _lab6APIService.GetAddressesAsync();
            return View(addresses);
        }

        // GET: Addresses/Details/{id}
        [Route("/addresses/details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var address = await _lab6APIService.GetAddressAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }
    }
}
