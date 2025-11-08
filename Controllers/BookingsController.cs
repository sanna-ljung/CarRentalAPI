using CarRentalAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CarRentalAPI.Data;

namespace CarRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Kräver JWT-token
    public class BookingsController : ControllerBase
    {
        private readonly CarRentalAPIContext _context;

        public BookingsController(CarRentalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Bookings/my
        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetMyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            int customerId = int.Parse(userId);

            var bookings = await _context.Booking
                .Include(b => b.Car)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();

            return bookings;
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] Booking booking)
        {
            if (booking.EndDate <= booking.StartDate)
                return BadRequest("Slutdatum måste vara efter startdatum.");

            // Hämta CustomerId från JWT-claim
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            booking.CustomerId = int.Parse(userId);

            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyBookings), new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null || booking.CustomerId != int.Parse(userId))
                return Forbid();

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
