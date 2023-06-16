using AutoMapper;
using BookingsWithMe.Data;
using BookingsWithMe.Entities;
using BookingsWithMe.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UsersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        return await _context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(UserForUpdateDto userForUpdateDto)
    {
        if (await UserExists(userForUpdateDto.Email))
            return BadRequest("User with that email is already exists");

        var user = _mapper.Map<User>(userForUpdateDto);

        _context.Entry(user).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        var userForDisplayDto = _mapper.Map<UserForDisplayDto>(user);

        return Ok(userForDisplayDto);
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(UserForCreationDto userForCreationDto)
    {
        var user = _mapper.Map<User>(userForCreationDto);
        if (_context.Users == null)
        {
            return Problem("Entity set 'AppDbContext.Users'  is null.");
        }
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email == email);
    }
}
