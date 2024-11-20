using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Lab6.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ApplicationContext _context;

        public TransactionsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _context.Transactions
                .Select(t => new
                {
                    AccountNumber = t.AccountNumber,
                    MerchantId = t.MerchantId,
                    TransactionTypeCode = t.TransactionTypeCode,
                    TransactionDateTime = t.TransactionDateTime,
                    TransactionAmount = t.TransactionAmount,
                    OtherDetails = t.OtherDetails ?? string.Empty
                })
                .ToListAsync();

            var json = JsonConvert.SerializeObject(transactions, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var transaction = await _context.Transactions
                .Where(t => t.TransactionId == id)
                .Select(t => new
                {
                    AccountNumber = t.AccountNumber,
                    MerchantId = t.MerchantId,
                    TransactionTypeCode = t.TransactionTypeCode,
                    TransactionDateTime = t.TransactionDateTime,
                    TransactionAmount = t.TransactionAmount,
                    OtherDetails = t.OtherDetails ?? string.Empty
                })
                .FirstOrDefaultAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            var json = JsonConvert.SerializeObject(transaction, Newtonsoft.Json.Formatting.Indented);
            return Content(json, "application/json");
        }

        //// API для пошуку транзакцій
        //[HttpGet("search")]
        //public async Task<IActionResult> SearchTransactions(DateTime? date, string? merchantId, string? transactionTypeCode)
        //{
        //    var query = _context.Transactions.AsQueryable();

        //    if (date.HasValue)
        //    {
        //        query = query.Where(t => t.TransactionDateTime.Date == date.Value.Date);
        //    }

        //    if (!string.IsNullOrEmpty(merchantId))
        //    {
        //        query = query.Where(t => t.MerchantId.Contains(merchantId));
        //    }

        //    if (!string.IsNullOrEmpty(transactionTypeCode))
        //    {
        //        query = query.Where(t => t.TransactionTypeCode == transactionTypeCode);
        //    }

        //    var transactions = await query
        //        .Select(t => new
        //        {
        //            AccountNumber = t.AccountNumber,
        //            MerchantId = t.MerchantId,
        //            TransactionTypeCode = t.TransactionTypeCode,
        //            TransactionDateTime = t.TransactionDateTime,
        //            TransactionAmount = t.TransactionAmount,
        //            OtherDetails = t.OtherDetails ?? string.Empty
        //        })
        //        .ToListAsync();

        //    var json = JsonConvert.SerializeObject(transactions, Newtonsoft.Json.Formatting.Indented);
        //    return Content(json, "application/json");
        //}
    }
}
