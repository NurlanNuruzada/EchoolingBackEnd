using Echooling.Aplication.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers;
[Route("api/info")]
[ApiController]
public class InfoController : ControllerBase
{
    private readonly IInfoService _infoService;

    public InfoController(IInfoService infoService)
    {
        _infoService = infoService;
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetInfo()
    {
        try
        {
            var info = await _infoService.getInfo();
            return Ok(info);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
