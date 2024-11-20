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
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CustomersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers
                .Select(c => new
                {
                    //CustomerId = c.CustomerId,
                    PersonalDetails = c.PersonalDetails ?? string.Empty,
                    ContactDetails = c.ContactDetails ?? string.Empty,
                    BranchId = c.BranchId,
                    AddressId = c.AddressId
                })
                .ToListAsync();

            var json = JsonConvert.SerializeObject(customers, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _context.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => new
                {
                    //CustomerId = c.CustomerId,
                    PersonalDetails = c.PersonalDetails ?? string.Empty,
                    ContactDetails = c.ContactDetails ?? string.Empty,
                    BranchId = c.BranchId,
                    AddressId = c.AddressId
                })
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            var json = JsonConvert.SerializeObject(customer, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }
    }
}
