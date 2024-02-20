using Echooling.Aplication.DTOs.AuthDTOs;
using Echooling.Aplication.DTOs.EmailDTOs;
using Echooling.Aplication.DTOs.ResponseDTOs;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface IAuthService
    {
        Task<IEnumerable<GetAllAppUsersDto>> GetAllUsersWithRolesAsync();
        Task<IList<string>> GetAllRoles();
        Task Register(RegisterDto registerDto);
        Task<TokenResponseDto> Login(SignInDto signInDto);
        Task<TokenResponseDto> ValidateRefreshToken(string refreshToken);
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
        Task ResetPasswordLetter(Guid id);
        Task ForgetPasswordLetter(string Identifier);
        Task<IList<string>> getUserRole(Guid userId);
        Task RemoveRole(Guid userId, string Role);
        Task AddRole(Guid userId, string Role);
    }
}
