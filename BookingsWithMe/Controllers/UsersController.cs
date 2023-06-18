using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.Models.User;
using BookingsWithMe.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace BookingsWithMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserForDisplayDto>>> GetUsers(
        [FromQuery] UserResourceParameters usersResourceParameters, CancellationToken ct)
    {
        var usersForDisplay = await _userService.GetUsersAsync(usersResourceParameters, ct);
        return Ok(usersForDisplay);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(Guid id, CancellationToken ct)
    {
        var userForDisplay = await _userService.GetUserAsync(id, ct);
        if (userForDisplay == null)
            return NotFound();

        return Ok(userForDisplay);
    }

    [HttpPost]
    public async Task<ActionResult<UserForDisplayDto>> PostUser(UserForCreationDto userForCreationDto,
        CancellationToken ct)
    {
        if (await _userService.EmailExistsAsync(userForCreationDto.Email, ct))
            return BadRequest("User with that email is already exists");

        var userForDisplay = await _userService.CreateUserAsync(userForCreationDto, ct);

        return CreatedAtAction("GetUser", new { id = userForDisplay.Id }, userForDisplay);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserForDisplayDto>> PutUser(UserForUpdateDto userForUpdateDto, CancellationToken ct)
    {
        var userForDisplayDto = await _userService.UpdateUserAsync(userForUpdateDto, ct);
        if (userForDisplayDto == null)
            return BadRequest("User with that Id is not found");

        return Ok(userForDisplayDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct)
    {
        if (!await _userService.DeleteUserAsync(id, ct))
            return NotFound();

        return NoContent();
    }
}