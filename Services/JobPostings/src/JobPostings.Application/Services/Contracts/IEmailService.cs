using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Application.Services.Contracts;

public interface IEmailService
{
    Task SendMail(EmailRequest message);
}