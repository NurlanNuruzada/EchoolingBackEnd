using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.ResponseDTOs;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Infrastructure.Services.Token
{
    public class TokenHandler:ITokenHandler
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenHandler(UserManager<AppUser> userManager,
                           IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<TokenResponseDto> CreateAccessToken(int addminutes,int refrestTokenMinutes, AppUser user)
        {
            List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName)
        };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            DateTime ExpireDate = DateTime.UtcNow.AddMinutes(120);
            JwtSecurityToken jwt = new(
                 audience: _configuration["JWT:Audience"],
                 issuer: _configuration["JWT:Issuer"],
                 claims: claims,
                 notBefore: DateTime.UtcNow,
                 expires: ExpireDate,
                 signingCredentials: Credentials
                 );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refrestToken = GenerateRefreshToken();
            return new TokenResponseDto(token, ExpireDate,DateTime.UtcNow.AddMinutes(refrestTokenMinutes), refrestToken,user.UserName,user.Fullname,user.Email);
        }
        private string GenerateRefreshToken()
        {
            byte[] bytes =new byte[64];
            var randomNumber = RandomNumberGenerator.Create();
            randomNumber.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
