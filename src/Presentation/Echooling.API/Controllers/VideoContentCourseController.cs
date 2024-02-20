using System.Net;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoContentCourseController : ControllerBase
    {
        readonly IVideoContentCourseService _videoContentCourseService;

        public VideoContentCourseController(IVideoContentCourseService videoContentCourseService)
        {
            _videoContentCourseService = videoContentCourseService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Guid CourseId, Guid VideoId)
        {
            await _videoContentCourseService.AddVideoToCourse(CourseId, VideoId);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
