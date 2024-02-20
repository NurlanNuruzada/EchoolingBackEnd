using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.NewsDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendNofication(NewsDto newsDto)
        {
            await _newsService.sentNews(newsDto);
            return Ok("user registered!");
        } 
        [HttpPost("[action]")]
        public async Task<IActionResult> ContactMessage(ContactUsDto message)
        {
            await _newsService.ContactUs(message);
            return Ok("message sent!");
        } 
    }
}
