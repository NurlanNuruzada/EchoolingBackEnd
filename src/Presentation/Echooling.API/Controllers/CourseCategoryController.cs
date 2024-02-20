using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Persistance.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Echooling.Aplication.DTOs.CategoryDTOs;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService _courseService;

        public CourseCategoryController(ICourseCategoryService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> get(Guid id)
        {
            CategoryGetDto slider = await _courseService.GetCourseCategoryById(id);
            return Ok(slider);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            List<CategoryGetDto> List = await _courseService.GetAllAsync();
            return Ok(List);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> delete( Guid id)
        {
            try
            {
                await _courseService.Remove(id);
            }
            catch (notFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, new { message = ex.Message });
            }
            return Ok(new { message = "Category deleted successfully." });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCategoryDto CourseCategoryDto)
        {
            await _courseService.CreateCourseCategory(CourseCategoryDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut("id")]
        public async Task<IActionResult> update([FromBody] CourseCategoryDto CourseCategoryDto, Guid id)
        {
            await _courseService.UpdateAsync(CourseCategoryDto, id);
            return Ok(new { message = "Event Updated successfully." + CourseCategoryDto });
        }
    }
}

