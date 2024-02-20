using System.Text;
using System.Web;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.AuthDTOs;
using Echooling.Aplication.DTOs.EmailDTOs;
using Echooling.Aplication.DTOs.ResponseDTOs;
using Echooling.Persistance.Contexts;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Helper;
using Ecooling.Domain.Entites;
using Ecooling.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Echooling.Persistance.Implementations.Services
{
    public class AuthService : IAuthService 
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenHandler _tokenHandler;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfirmEmail _confirmEmail;
        private readonly IOptions<SecurityStampValidatorOptions> _securityStampOptions;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           IConfiguration configuration,
                           ITokenHandler tokenHandler,
                           AppDbContext context,
                           IEmailService emailService,
                           IConfirmEmail confirmEmail,
                           IOptions<SecurityStampValidatorOptions> securityStampOptions,
                           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenHandler = tokenHandler;
            _context = context;
            _emailService = emailService;
            _confirmEmail = confirmEmail;
            _securityStampOptions = securityStampOptions;
            _roleManager = roleManager;
        }

        public async Task<IList<string>> getUserRole(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles =  await _userManager.GetRolesAsync(user);
            return roles.ToArray();
        }
        public async Task AddRole(Guid userId,string Role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
             await _userManager.AddToRoleAsync(user, Role);
        }   
        public async Task RemoveRole(Guid userId,string Role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
             await _userManager.RemoveFromRoleAsync(user, Role);
        }
        public async Task<IList<string>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.Select(role => role.Name).ToListAsync();
            return roles;
        }
        public async Task<IEnumerable<GetAllAppUsersDto>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<GetAllAppUsersDto>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userDto = new GetAllAppUsersDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = userRoles.ToList() 
                };

                usersWithRoles.Add(userDto);
            }

            return usersWithRoles;
        }
        public async Task<TokenResponseDto> Login(SignInDto signInDto)
        {

            AppUser appUser = await _userManager.FindByEmailAsync(signInDto.EmailOrUsername);

            if (appUser is null)
            {
                appUser = await _userManager.FindByNameAsync(signInDto.EmailOrUsername);
                if (appUser is null) { throw new SignInFailureException("sign-in Identifier or Password is Wrong!"); }
            }
            var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, signInDto.password, true);
            if (!signInResult.Succeeded)
            {
                throw new UserRegistrationException("sign-in Identifier or Password is Wrong!");
            }
            if (!appUser.isActive)
            {
                throw new UserNotActiveException("Your accound is Blocked!");
            }

            var tokenResponse = await _tokenHandler.CreateAccessToken(1, 2, appUser);
            appUser.RefrestToken = tokenResponse.RefreshToken;
            appUser.RefrestTokenExpiration = tokenResponse.RefreshTokenExpiration;
            await _userManager.UpdateAsync(appUser);
            return tokenResponse;
        }
        public async Task Register(RegisterDto registerDto)
        {
            AppUser appUser = new()
            {
                Fullname = registerDto.surname + " " + registerDto.name,
                PhoneNumber = registerDto.phoneNumber,
                UserName = registerDto.UserName,
                Email = registerDto.email,
                isActive = true,
            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerDto.password);
            if (!identityResult.Succeeded)
            {
                StringBuilder err = new();
                foreach (var error in identityResult.Errors)
                {
                    err.AppendLine(error.Description);
                }
                throw new UserRegistrationException(err.ToString());
            }
            var result = await _userManager.AddToRoleAsync(appUser, Roles.Member.ToString());
            if (!result.Succeeded)
            {
                StringBuilder err = new();
                foreach (var error in result.Errors)
                {
                    err.AppendLine(error.Description);
                }
                throw new UserRegistrationException(err.ToString());
            }
            if (result.Succeeded)
            {

                var FrontEndBase = "http://localhost:3000";
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                string codeHtmlVersion = HttpUtility.UrlEncode(token);
                var userIp = EmailConfigurations.GetUserIP().ToString();
                var confirmationUrl = $"{FrontEndBase}/Auth/ConfirmEmail?userId={appUser.Id}&token={codeHtmlVersion.ToString()}";


                TimeSpan tokenExpration = _securityStampOptions.Value.ValidationInterval;

                SentEmailDto ConfirmLetter = new SentEmailDto
                {
                    To = appUser.Email,
                    Subject = "Confirm Email Address",
                    body = $"<html><body>" +
                    $"<h1>Welcome , <span style='color: #3270fc;'>{appUser.Fullname}</span></h1>" +
                    $"<h2>Confirm Your Email</h2>" +
                    $"<p>Please confirm your email address by clicking <a href='{confirmationUrl}'>here</a>. If it's not you, you can ignore this email.</p>" +
                    $"<br/>" +
                    $"<h3>This token's lifetime is {tokenExpration}, and we received this from {userIp}</h3>" +
                    $"</body></html>"
                };
                _emailService.SendEmail(ConfirmLetter);
            }

        }
        public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var userId = resetPasswordDto.userId;
            var token = resetPasswordDto.token;
            var user = await _userManager.FindByIdAsync(userId);
            var password = resetPasswordDto.password;
            if (user is null )
            {
                throw new notFoundException("User not found!");
            }
            if (password is null || token is null)
            {
                throw new notFoundException("token or password is null!");
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPassResult.Succeeded)
            {
                throw new CouldntResetPasswordException("someting went wrong in token,user or password!");
            }
        }
        public async Task ResetPasswordLetter(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new notFoundException("User not found!");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user); // Await the token generation
            var FrontEndBase = "http://localhost:3000";
            var codeHtmlVersion = HttpUtility.UrlEncode(token);
            var userIp = EmailConfigurations.GetUserIP().ToString();
            var resetPasswordUrl = $"{FrontEndBase}/Auth/ResetPassword?userId={userId}&token={codeHtmlVersion.ToString()}";

            TimeSpan tokenExpiration = _securityStampOptions.Value.ValidationInterval;
            SentEmailDto resetPasswordEmail = new SentEmailDto
            {
                To = user.Email,
                Subject = "Reset Password",
                body = $"<html><body>" +
                       $"<h1  style='color: #3270fc;'>Hi,{user.Fullname}</h1>" +
                       $"<h1>There was a request to change your password!</h1>" +
                       $"<p>Please click <a href='{resetPasswordUrl}'>here</a> to reset your password.If you did not forget your password, please disregard this email</p>" +
                       $"<br/>" +
                       $"<h3>This link is valid for {tokenExpiration.TotalHours} hours, and we received this from {userIp}</h3>" +
                       $"</body></html>"
            };
            _emailService.SendEmail(resetPasswordEmail);
        }
        public async Task ForgetPasswordLetter(string Identifier)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(Identifier);
            if (appUser is null)
            {
                appUser = await _userManager.FindByNameAsync(Identifier);
                if (appUser is null) { throw new SignInFailureException("sign-in Identifier or Password is Wrong!"); }
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser); 
            var FrontEndBase = "http://localhost:3000";
            var codeHtmlVersion = HttpUtility.UrlEncode(token);
            var userIp = EmailConfigurations.GetUserIP().ToString();
            var resetPasswordUrl = $"{FrontEndBase}/Auth/ResetPassword?userId={appUser.Id}&token={codeHtmlVersion.ToString()}";

            TimeSpan tokenExpiration = _securityStampOptions.Value.ValidationInterval;
            SentEmailDto resetPasswordEmail = new SentEmailDto
            {
                To = appUser.Email,
                Subject = "Reset Password",
                body = $"<html><body>" +
                       $"<h1  style='color: #3270fc;'>Hi,{appUser.Fullname}</h1>" +
                       $"<h1>There was a request to change your password!</h1>" +
                       $"<p>Please click <a href='{resetPasswordUrl}'>here</a> to reset your password.If you did not forget your password, please disregard this email</p>" +
                       $"<br/>" +
                       $"<h3>This link is valid for {tokenExpiration.TotalHours} hours, and we received this from {userIp}</h3>" +
                       $"</body></html>"
            };
            _emailService.SendEmail(resetPasswordEmail);
        }
        public async Task<TokenResponseDto> ValidateRefreshToken(string refreshToken)
        {
            if (refreshToken is null)
            {
                throw new ArgumentNullException("Refrest token does not exist");
            }
            var user = await _context.Users.Where(u => u.RefrestToken == (refreshToken)).FirstOrDefaultAsync();
            if (user is null)
            {
                throw new notFoundException("user not found!");
            }
            if (user.RefrestTokenExpiration < DateTime.UtcNow)
            {
                throw new ArgumentNullException("Refrest token does not exist");
            }
            var tokenResponse = await _tokenHandler.CreateAccessToken(2, 1, user);
            user.RefrestToken = tokenResponse.RefreshToken;
            user.RefrestTokenExpiration = tokenResponse.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);
            return tokenResponse;
        }

    }
}
