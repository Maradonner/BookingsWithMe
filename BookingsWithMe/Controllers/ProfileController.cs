using Microsoft.AspNetCore.Mvc;

namespace BookingsWithMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadImage()
    {
        return BadRequest();
    }
}
