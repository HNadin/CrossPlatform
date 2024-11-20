using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Lab6.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TimeController : ControllerBase
    {
        // GET: api/time/convert
        [HttpGet("convert")]
        public IActionResult ConvertToUkraineTime([FromQuery] DateTime utcDateTime)
        {
            try
            {
                TimeZoneInfo ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");

                DateTime ukraineDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, ukraineTimeZone);

                string formattedDateTime = ukraineDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                return Ok(new { ConvertedUkraineTime = formattedDateTime });
            }
            catch (TimeZoneNotFoundException)
            {
                return BadRequest("Часовий пояс не знайдено.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка сервера: {ex.Message}");
            }
        }
    }
}