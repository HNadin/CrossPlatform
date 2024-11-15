using Microsoft.AspNetCore.Mvc;
using Lab13.Server.Models;
using System.Diagnostics;

namespace Lab13.Server.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("api/error")]
        public IActionResult GetError()
        {
            var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return BadRequest(model);
        }
    }
}