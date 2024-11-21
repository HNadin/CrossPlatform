using Lab6.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Lab6.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/v1/accounts
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAccountV1()
        {
            var accounts = await _context.Accounts
                .Select(a => new
                {
                    AccountNumber = a.AccountNumber,
                    AccountStatusCode = a.AccountStatusCode,
                    AccountTypeCode = a.AccountTypeCode,
                    CurrentBalance = a.CurrentBalance,
                    OtherDetails = a.OtherDetails ?? string.Empty
                })
                .ToListAsync();

            var json = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }

        // GET: api/v1/accounts/{id}
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAccountV1(int id)
        {
            var account = await _context.Accounts
                .Where(a => a.AccountNumber == id)
                .Select(a => new
                {
                    AccountNumber = a.AccountNumber,
                    AccountStatusCode = a.AccountStatusCode,
                    AccountTypeCode = a.AccountTypeCode,
                    CurrentBalance = a.CurrentBalance,
                    OtherDetails = a.OtherDetails ?? string.Empty
                })
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            var json = JsonConvert.SerializeObject(account, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }

        // GET: api/v2/accounts
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetAccountV2()
        {
            var accounts = await _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.RefAccountType)
                .Select(a => new
                {
                    AccountNumber = a.AccountNumber,
                    AccountStatusCode = a.AccountStatusCode,
                    AccountTypeCode = a.AccountTypeCode,
                    CurrentBalance = a.CurrentBalance,
                    AccountTypeDescription = a.RefAccountType != null ? a.RefAccountType.AccountTypeDescription : "N/A",
                    CustomerPersonalDetails = a.Customer != null ? a.Customer.PersonalDetails : "N/A",
                    OtherDetails = a.OtherDetails ?? string.Empty
                })
                .ToListAsync();

            var json = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }

        // GET: api/v2/accounts/{id}
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetAccountV2(int id)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.RefAccountType)
                .Where(a => a.AccountNumber == id)
                .Select(a => new
                {
                    AccountNumber = a.AccountNumber,
                    AccountStatusCode = a.AccountStatusCode,
                    AccountTypeCode = a.AccountTypeCode,
                    CurrentBalance = a.CurrentBalance,
                    AccountTypeDescription = a.RefAccountType != null ? a.RefAccountType.AccountTypeDescription : "N/A",
                    CustomerPersonalDetails = a.Customer != null ? a.Customer.PersonalDetails : "N/A",
                    OtherDetails = a.OtherDetails ?? string.Empty
                })
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            var json = JsonConvert.SerializeObject(account, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }
    }
}

