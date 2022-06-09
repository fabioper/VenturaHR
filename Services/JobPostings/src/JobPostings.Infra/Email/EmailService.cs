using System.Net;
using System.Net.Mail;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
using JobPostings.CrossCutting.Settings;

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
        smtp.EnableSsl = true;
        return smtp;
    }

    public async Task SendMail(EmailRequest message)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.From, "VenturaHR"),
            To = { new MailAddress(message.To) },
            Subject = message.Subject,
            Body = message.Body,
            IsBodyHtml = true,
            Priority = MailPriority.High
        };

        try
        {
            await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}