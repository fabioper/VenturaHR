namespace JobPostings.CrossCutting.Services.Email;

public interface IEmailService
{
    Task SendMail(string to, string subject, string body);
}