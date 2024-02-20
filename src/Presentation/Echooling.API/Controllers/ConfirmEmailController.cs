using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.EmailDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmEmailController : ControllerBase
    {
        private readonly IConfirmEmail _confirmEmail;

        public ConfirmEmailController(IConfirmEmail confirmEmail)
        {
            _confirmEmail = confirmEmail;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> ConfirmEmai(ConfirmEmailDto confirmEmailDto)
        {
            await _confirmEmail.ConfirmEmail(confirmEmailDto);
            return Ok("email Confirmed!");
        }
    }
}
