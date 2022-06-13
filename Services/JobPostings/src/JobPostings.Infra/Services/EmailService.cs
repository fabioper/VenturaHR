using System.Net;
using System.Net.Mail;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
using JobPostings.CrossCutting.Services.Email;
using JobPostings.CrossCutting.Settings;
using JobPostings.Domain.Services.Contracts;

namespace JobPostings.Infra.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly SmtpClient _smtpClient;

    public EmailService(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
        _smtpClient = CreateSmtpClient();
    }

    private SmtpClient CreateSmtpClient()
    {
        var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port);
        smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
        smtp.UseDefaultCredentials = false;
        smtp.EnableSsl = true;
        return smtp;
    }

    public async Task SendMail(string to, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.From, "VenturaHR"),
            To = { new MailAddress(to) },
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
            Priority = MailPriority.High
        };

        await _smtpClient.SendMailAsync(mailMessage);
    }
}