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
    public async Task<ActionResult<Availability>> GetAvailability(int id)
    {
        var availability = await _context.Availabilities.FindAsync(id);

        if (availability == null)
        {
            return NotFound();
        }

        return availability;
    }

    [HttpPost]
    public async Task<ActionResult> PostAvailability(AvailabilityForCreationDto availabilityForCreationDto)
    {
        var availability = _mapper.Map<Availability>(availabilityForCreationDto);

        _context.Availabilities.Add(availability);
        await _context.SaveChangesAsync();

        var availabilityForDisplayDto = _mapper.Map<AvailabilityForDisplayDto>(availability);

        return CreatedAtAction("GetAvailability", new { id = availability.Id }, availabilityForDisplayDto);
    }

    [HttpPut]
    public async Task<ActionResult> PutAvailability(AvailabilityForUpdateDto availabilityForUpdateDto)
    {
        var availability = _mapper.Map<Availability>(availabilityForUpdateDto);

        _context.Entry(availability).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        var availabilityForDisplayDto = _mapper.Map<AvailabilityForDisplayDto>(availability);

        return Ok(availabilityForDisplayDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAvailability(int id)
    {
        var availability = await _context.Availabilities.FindAsync(id);
        if (availability == null)
        {
            return NotFound();
        }

        _context.Availabilities.Remove(availability);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
