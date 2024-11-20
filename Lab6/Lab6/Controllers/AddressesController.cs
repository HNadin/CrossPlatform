using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Xml;
using Microsoft.AspNetCore.Authorization;

namespace Lab6.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AddressesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/addresses
        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var addresses = await _context.Addresses
                .Select(a => new
                {
                    //AddressId = a.AddressId,
                    Line1 = a.Line1 ?? string.Empty,
                    Line2 = a.Line2 ?? string.Empty,
                    TownCity = a.TownCity ?? string.Empty,
                    ZipPostcode = a.ZipPostcode ?? string.Empty,
                    StateProvinceCounty = a.StateProvinceCounty ?? string.Empty,
                    Country = a.Country ?? string.Empty,
                    OtherDetails = a.OtherDetails ?? string.Empty
                })
                .ToListAsync();

            var json = JsonConvert.SerializeObject(addresses, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }

        // GET: api/addresses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var address = await _context.Addresses
                .Where(a => a.AddressId == id)
                .Select(a => new
                {
                    //AddressId = a.AddressId,
                    Line1 = a.Line1 ?? string.Empty,
                    Line2 = a.Line2 ?? string.Empty,
                    TownCity = a.TownCity ?? string.Empty,
                    ZipPostcode = a.ZipPostcode ?? string.Empty,
                    StateProvinceCounty = a.StateProvinceCounty ?? string.Empty,
                    Country = a.Country ?? string.Empty,
                    OtherDetails = a.OtherDetails ?? string.Empty
                })
                .FirstOrDefaultAsync();

            if (address == null)
            {
                return NotFound();
            }

            var json = JsonConvert.SerializeObject(address, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }
    }
}
