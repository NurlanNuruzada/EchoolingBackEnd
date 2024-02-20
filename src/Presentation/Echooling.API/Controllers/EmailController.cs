using Echooling.Aplication.DTOs.EmailDTOs;
using Microsoft.AspNetCore.Mvc;
using Echooling.Aplication.Abstraction.Services;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("[action]")]
        public IActionResult MailSender(SentEmailDto request)
        {
            _emailService.SendEmail(request);
            return Ok("email sent");
        }
    }
}
