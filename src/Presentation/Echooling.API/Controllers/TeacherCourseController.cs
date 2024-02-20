using Echooling.Aplication.Abstraction.Services;
using Ecooling.Domain.Entites;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherCourseController : ControllerBase
    {
        private readonly ITeacherCourses _teacherCourseService;

        public TeacherCourseController(ITeacherCourses teacherCourseService)
        {
            _teacherCourseService = teacherCourseService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Guid CourseId, Guid TeaherId)
        {
            await _teacherCourseService.AddCourseToTeacherAsync(CourseId, TeaherId);
            return StatusCode((int)HttpStatusCode.Created); 
        }
    }
}
