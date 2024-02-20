using Echooling.Aplication.DTOs.StaffDTOs;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Echooling.Persistance.Exceptions;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppuserEventController : ControllerBase
    {
        private readonly IAppUserEventService _appUserEventService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEventService _eventService;
        public AppuserEventController(IAppUserEventService appUserEventService, UserManager<AppUser> userManager, IEventService eventService)
        {
            _appUserEventService = appUserEventService;
            _userManager = userManager;
            _eventService = eventService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUsertoEvent(AppUserEventDto appUserEventDto)
        {
            string guid = appUserEventDto.AppUserId.ToString();
            var Event = await _eventService.getById(appUserEventDto.eventsId);
            var user = await _userManager.FindByIdAsync(guid);
            if (user is null)
            {
                throw new notFoundException("user not found");
            }
            if (Event is null)
            {
                throw new notFoundException("event not found");
            }
            await _appUserEventService.CreateAsync(appUserEventDto);
                return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
