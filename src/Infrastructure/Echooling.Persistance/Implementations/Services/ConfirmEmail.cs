using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.EmailDTOs;
using Echooling.Persistance.Exceptions;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Persistance.Implementations.Services
{
    public class ConfirmEmail : IConfirmEmail
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmEmail(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        async Task IConfirmEmail.ConfirmEmail(ConfirmEmailDto confirmEmailDto)
        {
            var UserId = confirmEmailDto.userId;
            var Token = confirmEmailDto.token;
            if (UserId == null || Token == null)
            {
                throw new Exception("token or user is null");
            }
            var user =await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new notFoundException("user not found!");
            }
            var result = await _userManager.ConfirmEmailAsync(user, Token);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"UserId: {UserId}, Token: {Token}");
                    Console.WriteLine($"Error: {error.Code}, Description: {error.Description}");
                }
                throw new Exception("Email confirmation failed.");
            }
        }
    }
}
