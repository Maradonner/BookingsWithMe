using BookingsWithMe.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingsWithMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IPictureService _pictureService;
    private readonly ISessionService _sessionService;

    public ProfileController(IPictureService pictureService, ISessionService sessionService)
    {
        _pictureService = pictureService;
        _sessionService = sessionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMySession()
    {
        var session = await _sessionService.GetSessionAsync();
        return Ok(session);
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm(Name = "Picture")] IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (fileExtension != ".jpg" && fileExtension != ".png" && fileExtension != ".bmp")
            return BadRequest("Invalid image file extension.");

        var filename = _pictureService.GetWebFilename(file.FileName);
        var result = await _pictureService.UploadImage(file.OpenReadStream(), filename);

        if (result)
            return Ok();

        return BadRequest();
    }
}