using customerBookingAPI.Models;
using customerBookingAPI.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static customerBookingAPI.RequestModels.CreateBookingRequest;
using static customerBookingAPI.RequestModels.MoodifyBookingStatusRequest;

namespace customerBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController: ControllerBase
    {
        private readonly BookingDbContext _context;
        public BookingController(BookingDbContext context)
        {
            _context = context;
        }
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsByCustomer(int id)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Where(b => b.Customer.CustomerID == id)
                .ToListAsync();

            if (!bookings.Any())
            {
                return NotFound();
            }

            return bookings;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(CreateBookingRequest request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerID);

            if (customer == null)
            {
                return BadRequest("Customer not found.");
            }

            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.RoomID == request.RoomID);

            if (room == null)
            {
                return BadRequest("Room not found.");
            }

            if (!string.Equals(room.Status, "Available", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Room is not available.");
            }

            if (request.CheckIn >= request.CheckOut)
            {
                return BadRequest("Check-out date must be after check-in date.");
            }

            if(request.NumberOfGuests<=0)
            {
                return BadRequest("Invalid number of guests.");
            }


            int nights = request.CheckOut.DayNumber - request.CheckIn.DayNumber;

            var booking = new Booking
            {
                CustomerID = request.CustomerID,
                RoomID = request.RoomID,
                CheckIn = request.CheckIn,
                Checkout = request.CheckOut,
                Nights = nights,
                NumberOfGuests = request.NumberOfGuests,
                TotalCost = nights * room.RoomType.PricePerNight, // Update this if your price is stored elsewhere
                BookingStatus = "Pending",
                BookingDate = DateTime.Now
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingsByCustomer), new { id = booking.BookingID }, booking);
        }
        [HttpPatch("{id}/status")]
        public async Task<ActionResult> PatchBookingStatus(int id, MoodifyBookingStatusRequest request)
        {

            var validStatuses = new[] { "Pending", "Confirmed", "Cancelled" };
            if (!validStatuses.Contains(request.BookingStatus))
            {
                return BadRequest("Invalid booking status.");
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.BookingStatus = request.BookingStatus;

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
