using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Target.Application.Interfaces;
using Target.Domain.Dtos;

namespace Target.Application.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;

    public EmailService(IConfiguration configuration)
    {
        var port = int.Parse(configuration["SmtpCredentials:Port"]);
        var username = configuration["SmtpCredentials:Username"];
        var password = configuration["SmtpCredentials:Password"];
        var host = configuration["SmtpCredentials:Host"];
        var ssl = bool.Parse(configuration["SmtpCredentials:EnableSSL"]);

        _smtpClient = new SmtpClient(host)
        {
            Port = port,
            Credentials = new NetworkCredential(username, password),
            EnableSsl = ssl
        };
    }
    public void SendEmailAsync(EmailDto emailDto)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailDto.From),
                Subject = emailDto.Subject,
                Body = emailDto.Body
            };

            mailMessage.To.Add(emailDto.To);
            _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            // Log the exception
           throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}
