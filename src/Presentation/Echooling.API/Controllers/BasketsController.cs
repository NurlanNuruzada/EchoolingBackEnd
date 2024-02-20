using System.Net;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.StaffDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : ControllerBase
{
    private readonly IBasketService _basketService;
    public BasketsController(IBasketService basketService)
        => _basketService = basketService;

    [HttpPost]
    public async Task<IActionResult> CreateStaff(Guid id, string AppUserId)
    {
        await _basketService.AddBasketAsync(id,AppUserId );
        return StatusCode((int)HttpStatusCode.Created);
    }
}
