using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.AuthDTOs;
using Echooling.Aplication.DTOs.EmailDTOs;
using Ecooling.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(SignInDto signInDto)
        {
            var response = await _authService.Login(signInDto);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshToken([FromQuery] string token)
        {
            var response = await _authService.ValidateRefreshToken(token);
            return Ok(response);
        }   
        [HttpGet("[action]")]
        public async Task<IList<string>> getUserRole(Guid userId)
        {
            var response = await _authService.getUserRole(userId);
            return (response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await _authService.Register(registerDto);
            return Ok("User registered successfully");
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(Guid userId, string Role)
        {
            await _authService.AddRole(userId, Role);
            return Ok();
        }

        [HttpPost("RemoveRole")]
        public async Task<IActionResult> RemoveRole(Guid userId, string Role)
        {
            await _authService.RemoveRole(userId, Role);
            return Ok();
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _authService.GetAllRoles();
            return Ok(roles);
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var usersWithRoles = await _authService.GetAllUsersWithRolesAsync();
            return Ok(usersWithRoles);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> ResetPasswordLetter(Guid id)
        {
            await _authService.ResetPasswordLetter(id);
            return Ok("Lettter sent!");
        }
        [HttpPost("[action]/{Identifier}")]
        public async Task<IActionResult> ForgetPasswordLetter(string Identifier)
        {
            await _authService.ForgetPasswordLetter(Identifier);
            return Ok("Lettter sent!");
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            await _authService.ResetPassword(resetPasswordDto);
            return Ok("password Changed!");
        }
    }
}
