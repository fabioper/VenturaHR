using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Aggregates.JobPostings;
using JobPostings.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace JobPostings.Application.Services.Concretes;

public class ExpiringJobsService : IExpiringJobsNotifierService
{
    private readonly IJobPostingRepository _jobPostingRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<ExpiringJobsService> _logger;

    public ExpiringJobsService(
        IJobPostingRepository jobPostingRepository, IEmailService emailService, ILogger<ExpiringJobsService> logger)
    {
        _jobPostingRepository = jobPostingRepository;
        _emailService = emailService;
        _logger = logger;
    }

    private async Task<List<JobPosting>> GetExpiringJobs()
    {
        _logger.LogInformation("Searching for jobs about to expire");
        var expiringJobs = (await _jobPostingRepository.GetAllJobsAboutToExpire()).ToList();

        _logger.LogInformation($"Found {expiringJobs.Count} job postings about to expire.");
        return expiringJobs;
    }

    public async Task NotifyCompaniesOfExpiringJobs()
    {
        var expiringJobs = await GetExpiringJobs();

        if (!expiringJobs.Any())
            return;

        _logger.LogInformation("Notifying companies.");

        foreach (var emailMessage in GetExpiringJobEmailRequest(expiringJobs))
            await _emailService.SendMail(emailMessage);
    }

    private static IEnumerable<EmailRequest> GetExpiringJobEmailRequest(IEnumerable<JobPosting> expiringJobs)
    {
        foreach (var expiringJob in expiringJobs)
        {
            const string title = "Vaga prestes à expirar";
            var body = $"A vaga {expiringJob.Title} está prevista para expirar em {expiringJob.ExpireAt.ToShortDateString()}.";
            yield return new EmailRequest(expiringJob.Company.Email, title, body);
        }
    }
}