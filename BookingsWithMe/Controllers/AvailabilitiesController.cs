using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingsWithMe.Data;
using BookingsWithMe.Entities;
using BookingsWithMe.Models.Availabilitie;
using System.ComponentModel;
using AutoMapper;

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

    // GET: api/Availabilities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilities()
    {
        if (_context.Availabilities == null)
        {
            return NotFound();
        }
        return await _context.Availabilities.ToListAsync();
    }

    // GET: api/Availabilities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Availability>> GetAvailability(int id)
    {
        if (_context.Availabilities == null)
        {
            return NotFound();
        }
        var availability = await _context.Availabilities.FindAsync(id);

        if (availability == null)
        {
            return NotFound();
        }

        return availability;
    }

    // PUT: api/Availabilities/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAvailability(int id, Availability availability)
    {
        if (id != availability.Id)
        {
            return BadRequest();
        }

        _context.Entry(availability).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AvailabilityExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Availabilities
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Availability>> PostAvailability(AvailabilityForCreationDto availabilityForCreationDto)
    {
        var availability = _mapper.Map<Availability>(availabilityForCreationDto);

        if (_context.Availabilities == null)
        {
            return Problem("Entity set 'AppDbContext.Availabilities'  is null.");
        }
        _context.Availabilities.Add(availability);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAvailability", new { id = availability.Id }, availability);
    }

    // DELETE: api/Availabilities/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAvailability(int id)
    {
        if (_context.Availabilities == null)
        {
            return NotFound();
        }
        var availability = await _context.Availabilities.FindAsync(id);
        if (availability == null)
        {
            return NotFound();
        }

        _context.Availabilities.Remove(availability);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AvailabilityExists(int id)
    {
        return (_context.Availabilities?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
