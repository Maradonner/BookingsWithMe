using AutoMapper;
using BookingsWithMe.Data;
using BookingsWithMe.Entities;
using BookingsWithMe.Models.Availabilitie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvailabilitiesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AvailabilitiesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilities()
    {
        return await _context.Availabilities.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAvailability(Guid id)
    {
        var availability = await _context.Availabilities.FindAsync(id);

        if (availability == null)
        {
            return NotFound();
        }

        var availabilityForDisplay = _mapper.Map<AvailabilityForDisplayDto>(availability);

        return Ok(availabilityForDisplay);
    }

    [HttpPost]
    public async Task<ActionResult> PostAvailability(AvailabilityForCreationDto availabilityForCreationDto)
    {
        var user = await _context.Users.FindAsync(availabilityForCreationDto.UserId);

        if (user == null)
            return BadRequest("User with that id is not exists");

        var availability = _mapper.Map<Availability>(availabilityForCreationDto);

        await _context.Availabilities.AddAsync(availability);
        await _context.SaveChangesAsync();


        var availabilityForDisplayDto = _mapper.Map<AvailabilityForDisplayDto>(availability);

        return CreatedAtAction("GetAvailability", new { id = availability.Id }, availabilityForDisplayDto);
    }

    [HttpPut]
    public async Task<ActionResult> PutAvailability(AvailabilityForUpdateDto availabilityForUpdateDto)
    {
        var availability = _mapper.Map<Availability>(availabilityForUpdateDto);

        _context.Availabilities.Update(availability);
        await _context.SaveChangesAsync();

        var availabilityForDisplayDto = _mapper.Map<AvailabilityForDisplayDto>(availability);

        return Ok(availabilityForDisplayDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAvailability(int id)
    {
        var availability = await _context.Availabilities.FindAsync(id);
        if (availability == null)
            return NotFound();

        _context.Availabilities.Remove(availability);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
