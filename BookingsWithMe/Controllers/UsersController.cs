using AutoMapper;
using BookingsWithMe.Data;
using BookingsWithMe.Entities;
using BookingsWithMe.Helpers;
using BookingsWithMe.Models.User;
using BookingsWithMe.ResourceParameters;
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
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers(
        [FromQuery] UsersResourceParameters usersResourceParameters, CancellationToken ct)
    {
        var collection = _context.Users as IQueryable<User>;

        var pagedList = await PagedList<User>.CreateAsync(collection, usersResourceParameters.PageNumber, usersResourceParameters.PageSize, ct);

        var usersForDisplay = _mapper.Map<List<UserForDisplayDto>>(pagedList);

        return Ok(usersForDisplay);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(Guid id, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync(id, ct);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> PostUser(UserForCreationDto userForCreationDto, CancellationToken ct)
    {
        if (await EmailExists(userForCreationDto.Email, ct))
            return BadRequest("User with that email is already exists");

        var user = _mapper.Map<User>(userForCreationDto);

        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            user.Availabilities.Add(new Availability
            {
                Day = day,
                UserId = user.Id,
            });
        }

        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);

        var userForDisplay = _mapper.Map<UserForDisplayDto>(user);

        return CreatedAtAction("GetUser", new { id = user.Id }, userForDisplay);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutUser(UserForUpdateDto userForUpdateDto, CancellationToken ct)
    {
        var user = _mapper.Map<User>(userForUpdateDto);

        _context.Entry(user).State = EntityState.Modified;

        await _context.SaveChangesAsync(ct);

        var userForDisplayDto = _mapper.Map<UserForDisplayDto>(user);

        return Ok(userForDisplayDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync(id, ct);

        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(ct);

        return NoContent();
    }

    private async Task<bool> EmailExists(string email, CancellationToken ct)
    {
        return await _context.Users.AnyAsync(x => x.Email == email, ct);
    }
}
