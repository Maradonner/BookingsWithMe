using AutoMapper;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.DAL.Data;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.Models.Booking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public BookingsController(AppDbContext context, ISessionService sessionService, IMapper mapper)
        {
            _context = context;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Booking>> GetBooking(Guid id)
        {
            var booking = await _context.Bookings
                .AsNoTracking()
                .Include(x => x.Creator)
                .Include(x => x.User)
                .Include(x => x.Availability)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(booking == null)
                return NotFound();

            return booking;
        }


        [HttpPost]
        public async Task<ActionResult<Booking>> ScheduleBooking(BookingForCreationDto bookingForCreationDto)
        {
            var availability = await _context.Availabilities
                .FirstOrDefaultAsync(x => x.Id == bookingForCreationDto.AvailabilityId);

            if (availability == null)
                return BadRequest("The requested time slot is not available.");

            var booking = _mapper.Map<Booking>(bookingForCreationDto);

            var session = await _sessionService.GetSessionAsync();

            if (session == null)
                return Unauthorized("Session not found");

            availability.IsBlocked = true;
            booking.UserId = session.Id;
            booking.Status = "Scheduled";

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }


        [HttpDelete]
        public async Task<Booking> CancelBookingAsync(Guid bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("The booking with the given ID was not found.");
            }

            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();

            return booking;
        }

        [HttpPut]
        public async Task<Booking> CompleteBookingAsync(Guid bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException("The booking with the given ID was not found.");
            }

            booking.Status = "Completed";
            await _context.SaveChangesAsync();

            return booking;
        }
    }
}
