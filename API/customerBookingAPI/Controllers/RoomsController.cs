using Azure.Core;
using customerBookingAPI.Models;
using customerBookingAPI.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static customerBookingAPI.RequestModels.ModifyRoomStatusRequest;

namespace customerBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly BookingDbContext _context;

        public RoomsController(BookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms(string? status)
        {
            var query = _context.Rooms
                .Include(r => r.RoomType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                status = char.ToUpper(status[0]) + status.Substring(1).ToLower();

                query = query.Where(r => r.Status == status);
            }
            return await query.ToListAsync();
        }

        [HttpGet("{id}")] //view a specific room by ID
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.Include(r => r.RoomType).FirstOrDefaultAsync(r => r.RoomID == id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchRoom(int id, [FromBody] ModifyRoomStatusRequest RoomStatusRequest)
        {
            var validStatuses = new[] { "Available", "Booked", "Maintainance"};
            if (!validStatuses.Contains(RoomStatusRequest.RoomStatus))
            {
                return BadRequest("Invalid booking status.");
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            room.Status = RoomStatusRequest.RoomStatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Rooms.Any(r => r.RoomID == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

    }
}
