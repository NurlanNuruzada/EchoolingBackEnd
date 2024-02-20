using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.CategoryDTOs;
using Echooling.Persistance.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoryController : ControllerBase
    {
        private readonly IEventsCategoryService _eventService;

        public EventCategoryController(IEventsCategoryService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> get(Guid id)
        {
            CategoryGetDto slider = await _eventService.getById(id);
            return Ok(slider);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            List<CategoryGetDto> List = await _eventService.GetAllAsync();
            return Ok(List);
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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventCategoryDto EventCategoryDto)
        {
            await _eventService.Create(EventCategoryDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut("id")]
        public async Task<IActionResult> update([FromBody] EventCategoryDto EventCategoryDto, Guid id)
        {
            await _eventService.UpdateAsync(EventCategoryDto, id);
            return Ok(new { message = "Event Updated successfully." + EventCategoryDto });
        }
    }
}
