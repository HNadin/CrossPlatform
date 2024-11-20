using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Xml;
using Microsoft.AspNetCore.Authorization;

namespace Lab6.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ApplicationContext _context;

        public SearchController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/search
        [HttpGet]
        public async Task<IActionResult> Search(DateTime? date, [FromQuery] List<string>? transactionTypes, string? valueStart, string? valueEnd)
        {
            var query = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.RefTransactionType)
                .AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(t => t.TransactionDateTime.Date == date.Value.Date);
            }

            if (transactionTypes != null && transactionTypes.Any())
            {
                query = query.Where(t => transactionTypes.Contains(t.TransactionTypeCode));
            }

            if (!string.IsNullOrEmpty(valueStart))
            {
                query = query.Where(t => t.RefTransactionType.TransactionTypeDescription.StartsWith(valueStart));
            }

            if (!string.IsNullOrEmpty(valueEnd))
            {
                query = query.Where(t => t.RefTransactionType.TransactionTypeDescription.EndsWith(valueEnd));
            }

            var result = await query
                .Select(t => new
                {
                    AccountNumber = t.AccountNumber,
                    MerchantId = t.MerchantId,
                    TransactionTypeCode = t.TransactionTypeCode,
                    TransactionTypeDescription = t.RefTransactionType.TransactionTypeDescription,
                    TransactionDateTime = t.TransactionDateTime,
                    TransactionAmount = t.TransactionAmount,
                    OtherDetails = t.OtherDetails ?? string.Empty,
                })
                .ToListAsync();

            var json = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

            return Content(json, "application/json");
        }
    }
}
