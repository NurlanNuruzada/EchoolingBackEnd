using System.Net;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Persistance.Implementations.Services;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        public LoggerController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            List<CreateLogDto> List = await _loggerService.getAllAsync();
            return Ok(List);
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Create(CreateLogDto log)
        {
            await _loggerService.CreateLog(log);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
