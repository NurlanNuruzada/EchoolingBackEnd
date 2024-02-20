using Echooling.Aplication.DTOs.EventDTOs;
using Echooling.Persistance.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecooling.Domain.Entites;
using Echooling.Aplication.Abstraction.Services;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStaffController : ControllerBase
    {
        public readonly IStaffEventsService _staffEventsService;
        public EventStaffController(IStaffEventsService staffEventsService)
        {
            _staffEventsService = staffEventsService;
        }
        [HttpGet("[Action]/id")]
        public async Task<IActionResult> get(Guid id)
        {
            var staffEvents = await _staffEventsService.GetByEventOrStaffId(id);
            return Ok(staffEvents);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            List<Staff_Events> List = await _staffEventsService.GetAllAsync();
            return Ok(List);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Guid eventId, Guid staffId)
        {
            await _staffEventsService.AddStaffToEventAsync(eventId,staffId);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut("id")]
        public async Task<IActionResult> update([FromBody] Staff_Events eventDto, Guid id)
        {
            await _staffEventsService.UpdateAsync(eventDto, id);
            return Ok(new { message = "Updated successfully." + eventDto });
        }
    }
}
