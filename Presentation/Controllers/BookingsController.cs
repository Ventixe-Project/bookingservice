using Microsoft.AspNetCore.Mvc;
using Presentation.Interfaces;
using Presentation.Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(IBookingService bookingService) : ControllerBase
    {
        private readonly IBookingService _bookingService = bookingService;

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookingService.CreateBookingAsync(request);
            return result.Success
                ? Ok(new { id = result.BookingId })
                : StatusCode(StatusCodes.Status500InternalServerError, "Unable to create booking.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _bookingService.GetBookingByIdAsync(id);

            if (result == null || !result.Success || result.Result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
