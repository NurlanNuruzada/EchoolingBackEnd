using Echooling.Persistance.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.CourseReviewDTOs;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseReviewController : ControllerBase
    {
        private readonly ICourseReviewServices _courseReviewServices;

        public CourseReviewController(ICourseReviewServices courseReviewServices)
        {
            _courseReviewServices = courseReviewServices;
        }
        [HttpGet("id")]
        public async Task<IActionResult> get(Guid id)
        {
            var Review = await _courseReviewServices.getbyId(id);
            return Ok(Review);
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> getReviewsOfCourseById(Guid CourseId)
        {
            List<GetCourseReviewDto> List = await _courseReviewServices.getReviewsOfCourseById(CourseId);
            return Ok(List);
        }
        [HttpGet("[Action]/id")]
        public async Task<IActionResult> getReviewsOfCourseById2(Guid CourseId)
        {
            List<GetCourseReviewDto> List = await _courseReviewServices.getReviewsOfCourseById(CourseId);
            return Ok(List);
        }
        [HttpDelete("[Action]/id")]
        public async Task<IActionResult> delete(Guid reviewId, Guid UserId)
        {
            try
            {
                await _courseReviewServices.Delete(reviewId , UserId);
            }
            catch (notFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, new { message = ex.Message });
            }
            return Ok(new { message = "Review deleted successfully." });
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Create([FromBody] CreateCourseReviewDto createCourseReviewDto)
        {
            await _courseReviewServices.AddReview(createCourseReviewDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut("id")]
        public async Task<IActionResult> update(UpdateCourseReviewDto ReviewCreateDto, Guid reviewId,Guid UserId)
        {
            await _courseReviewServices.UpdateAsync(ReviewCreateDto, reviewId, UserId);
            return Ok(new { message = "Review Updated successfully." + ReviewCreateDto });
        }
    }
}
