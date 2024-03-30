using MeetingRoomBooking.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Controllers
{
    [ApiController]
    [Route("api/meetingRoomBooking/Booking")]
    public class MeetingRoomBookingBookingController : ControllerBase
    {
        private readonly MeetingRoomBookingContext _context;
        //private const string MESSAGEFORACCEPTADMINSTATUS = "/addADM";

        public MeetingRoomBookingBookingController(MeetingRoomBookingContext context)
        {
            _context = context;
        }

        [HttpGet("GetBookings")]
        public async Task<ActionResult<List<Booking>>> GetBookings()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return bookings;
            //return Ok(bookings);
        }

        [HttpGet("GetBookingsByFloor")]
        public async Task<ActionResult<List<Booking>>> GetBookingsByFloor(int floor)
        {
            var bookingsByFloor = await _context.Bookings
                .Include(b => b.MeetingRoom)
                .Where(b => b.MeetingRoom.Floor == floor)
                .ToListAsync();

            if (bookingsByFloor.Count == 0)
            {
                return NotFound("No bookings found for the specified floor");
            }

            return bookingsByFloor;
        }

        [HttpGet("GetBookingsByMeetingRoomId")]
        public async Task<ActionResult<List<Booking>>> GetBookingsByMeetingRoomId(int meetingRoomId)
        {
            var bookingsByMeetingRoomId = await _context.Bookings
                .Include(b => b.MeetingRoom)
                .Where(b => b.MeetingRoom.MeetingRoomId == meetingRoomId)
                .ToListAsync();

            if (bookingsByMeetingRoomId.Count == 0)
            {
                return NotFound("No bookings found for the specified meeting room");
            }

            return bookingsByMeetingRoomId;
        }

        [HttpGet("GetBookingsByUserId")]
        public async Task<ActionResult<List<Booking>>> GetBookingsByUserId(long userId)
        {
            var bookingsByUserId = await _context.Bookings
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            if (bookingsByUserId.Count == 0)
            {
                return NotFound("No bookings found for the specified user");
            }

            return bookingsByUserId;
        }

        [HttpDelete("CanceleBooking")]
        public async Task<ActionResult> CanceleBooking(int bookingId)
        {
            var bookingToDelete = await _context.Bookings.FindAsync(bookingId);

            if (bookingToDelete == null)
            {
                return NotFound("Booking not found");
            }

            _context.Bookings.Remove(bookingToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
