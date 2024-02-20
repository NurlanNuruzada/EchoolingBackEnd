using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Persistance.Exceptions;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        [HttpGet("id")]
        public async Task<IActionResult> get(Guid id)
        {
            SliderGetDto slider = await _sliderService.getById(id);
            return Ok(slider);
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> getAll()
        {
            List<SliderGetDto> List = await _sliderService.GetAllAsync();
            return Ok(List);
        }
        [HttpDelete("[Action]/id")]
        public async Task<IActionResult> delete(Guid id)
        {
            try
            {
                await _sliderService.Remove(id);
            }
            catch (notFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, new { message = ex.Message });
            }
            return Ok(new { message = "Category deleted successfully." });
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Create([FromForm]SliderCreateDto sliderCreateDto)
        {
            await _sliderService.CreateAsync(sliderCreateDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut("id")]
        public async Task<IActionResult> update([FromForm] SliderUpdateDto sldierUpdateDto, Guid id)
        {
            await _sliderService.UpdateAsync(sldierUpdateDto, id);
            return Ok(new { message = "Slider Updated successfully." + sldierUpdateDto });
        }
    }
}
