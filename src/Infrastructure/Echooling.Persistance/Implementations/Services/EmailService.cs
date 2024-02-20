using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.EmailDTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Echooling.Persistance.Implementations.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(SentEmailDto request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailSender:EmailUserName"]));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = request.body };

        using var smtp = new SmtpClient();
        smtp.Connect(_configuration["EmailSender:EmailHost"], 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_configuration["EmailSender:EmailUserName"], _configuration["EmailSender:EmailAppPassword"]);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}