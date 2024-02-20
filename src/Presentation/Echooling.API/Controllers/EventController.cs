using System.Net;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.EventDTOs;
using Echooling.Persistance.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.IO;
namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet("id")]
        public async Task<IActionResult> get(Guid id)
        {
            EventGetDto events = await _eventService.getById(id);
            return Ok(events);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            List<EventGetDto> List = await _eventService.GetAllAsync();
            return Ok(List);
        }
        [HttpGet("[Action]")]
        public async Task<List<EventGetDto>> GetAllAsyncTake(int take)
        {
            List<EventGetDto> List = await _eventService.GetAllAsyncTake(take);
            return List;
        }
        [HttpDelete("id")]
        public async Task<IActionResult> delete(Guid id)
        {
            try
            {
                await _eventService.Remove(id);
            }
            catch (notFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, new { message = ex.Message });
            }
            return Ok(new { message = "Category deleted successfully." });
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> BuyEvent(Guid EventId, Guid appUserId)
        {
            await _eventService.BuyEvent(EventId, appUserId);
            return Ok("Succesfully bougth");
        }



        [HttpPost("[Action]/id")]
        public async Task<IActionResult> Create([FromForm] EventCreateDto eventDto, Guid staffId)
        {
            await _eventService.CreateAsync(eventDto, staffId);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpGet("SearchCourse")]
        public async Task<IActionResult> SerarchEvent(string? EventName,
                                                         string? category,
                                                         DateTime? StartDate,
                                                         DateTime? EndDate,
                                                         string? location)
        {
            var List = await _eventService.GetAllSearchAsync(EventName, category, StartDate, EndDate, location);
            return Ok(List);
        }

        [HttpPut("id")]
        public async Task<IActionResult> update([FromForm] EventCreateDto eventDto, Guid id)
        {
            await _eventService.UpdateAsync(eventDto, id);
            return Ok(new { message = "Slider Updated successfully." + eventDto });
        }
        [HttpGet("[Action]/id")]
        public async Task<List<EventGetDto>> getEventsbyStaffId(Guid StaffId)
        {
            var List = await _eventService.getEventsbyStaffId(StaffId);
            return List;
        }
        [HttpGet("[Action]")]
        public async Task<List<GetBouthEventDto>> GetBouthEvents(Guid appUserId)
        {
            List<GetBouthEventDto> List = await _eventService.GetBouthEvent(appUserId);
            return List;
        }

    }

}

