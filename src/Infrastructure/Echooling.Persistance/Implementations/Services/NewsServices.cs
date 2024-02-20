using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.EmailDTOs;
using Echooling.Aplication.DTOs.NewsDTOs;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Helper;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Xml.Linq;

namespace Echooling.Persistance.Implementations.Services
{
    public class NewsServices : INewsService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public NewsServices(UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task sentNews(NewsDto newsDto)
        {
            var email = newsDto.mail.ToString();
            var user = await _userManager.FindByEmailAsync(email);
            if (newsDto is null)
            {
                throw new notFoundException(" not found!");
            }
            if (user is null)
            {
                throw new notFoundException("User not found!");
            }

            user.IsSendNewsConfirmed = true;
            var result = await _userManager.UpdateAsync(user);
            var FrontEndBase = "http://localhost:3000";
            var userIp = EmailConfigurations.GetUserIP().ToString();
            var resetPasswordUrl = $"{FrontEndBase}";
            DateTime datetimeNow = DateTime.Now;
           
            SentEmailDto subscriptinon = new SentEmailDto
            {

                To = email,
                Subject = "Welcome to Echooling",
                body = $"<html><body>" +
            $"<h1  style='color: #3270fc;'>Hi,{user.Fullname}</h1>" +
                           $"<h1> Thanks for subscribing to Echooling!</h1>" +
                           $"<p> As a subscriber, you will also receive the latest news and updates from Echooling.Stay informed about our exciting developments, promotions, and more.\r\n</p>" +
                           $"<br/>" +
                                $"<p>You can return from <a href='{FrontEndBase}'>here</a></p>" +
                           $"<h3>we received this from {userIp}</h3>" +
                           $"</body></html>"
            };
            _emailService.SendEmail(subscriptinon);
        }
        public async Task ContactUs(ContactUsDto Contact)
        {
            var email = Contact.Email.ToString();
            string Fullname = Contact.Surname + " " + Contact.Name;
            if (email is null)
            {
                throw new notFoundException(" not found!");
            }
            var FrontEndBase = "http://localhost:3000";
            var userIp = EmailConfigurations.GetUserIP().ToString();
            var resetPasswordUrl = $"{FrontEndBase}";
            DateTime datetimeNow = DateTime.Now;
            SentEmailDto subscriptinon = new SentEmailDto
            {
                To = "nurlangn@code.edu.az",
                Subject = Contact.Subject,
                body = $"<html><body>" +
            $"<h1  style='color: #3270fc;'>Hi, message from {Fullname}</h1>" +
                           $"<h1>the message content:</h1>" +
                           $"<p> {Contact.Message}\r\n</p>" +
                           $"<br/>" +
                           $"<h3>we received this from {userIp}</h3>" +
                           $"</body></html>"
                           + $"<h1  style='color: #3270fc;'>Email: {Contact.Email}</h1>"

            };
            _emailService.SendEmail(subscriptinon);
        }
    }
}
